using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Windows.Devices.I2c;
using Windows.Devices.Enumeration;
using System.Threading.Tasks;

namespace MaEMMDataAccessLogic
{
    public class ADC
    {
        private byte _deviceAddress, _bitShift;
        private UInt16 _gain;
        private UInt16 _conversionDelay;
        private I2cDevice htu21dSensor;

        /*=========================================================================
        I2C ADDRESS/BITS
        -----------------------------------------------------------------------*/
        private const byte ADS1015_ADDRESS = 0x48;    // 1001 000 (ADDR = GND)
        /*=========================================================================*/

        /*=========================================================================
        CONVERSION DELAY (in mS)
        -----------------------------------------------------------------------*/
        private const byte ADS1015_CONVERSIONDELAY = 1;
        private const byte ADS1115_CONVERSIONDELAY = 8;
        /*=========================================================================*/

        /*=========================================================================
        POINTER REGISTER
        -----------------------------------------------------------------------*/
        private const byte ADS1015_REG_POINTER_MASK = 0x03;
        private const byte ADS1015_REG_POINTER_CONVERT = 0x00;
        private const byte ADS1015_REG_POINTER_CONFIG = 0x01;
        private const byte ADS1015_REG_POINTER_LOWTHRESH = 0x02;
        private const byte ADS1015_REG_POINTER_HITHRESH = 0x03;
        /*=========================================================================*/

        /*=========================================================================
        CONFIG REGISTER
        -----------------------------------------------------------------------*/
        private const UInt16 ADS1015_REG_CONFIG_OS_MASK = 0x8000;
        private const UInt16 ADS1015_REG_CONFIG_OS_SINGLE = 0x8000;  // Write: Set to start a single-conversion
        private const UInt16 ADS1015_REG_CONFIG_OS_BUSY = 0x0000;  // Read: Bit = 0 when conversion is in progress
        private const UInt16 ADS1015_REG_CONFIG_OS_NOTBUSY = 0x8000;  // Read: Bit = 1 when device is not performing a conversion

        private const UInt16 ADS1015_REG_CONFIG_MUX_MASK = 0x7000;
        private const UInt16 ADS1015_REG_CONFIG_MUX_DIFF_0_1 = 0x0000;  // Differential P = AIN0, N = AIN1 (default)
        private const UInt16 ADS1015_REG_CONFIG_MUX_DIFF_0_3 = 0x1000;  // Differential P = AIN0, N = AIN3
        private const UInt16 ADS1015_REG_CONFIG_MUX_DIFF_1_3 = 0x2000; // Differential P = AIN1, N = AIN3
        private const UInt16 ADS1015_REG_CONFIG_MUX_DIFF_2_3 = 0x3000;  // Differential P = AIN2, N = AIN3
        private const UInt16 ADS1015_REG_CONFIG_MUX_SINGLE_0 = 0x4000;  // Single-ended AIN0
        private const UInt16 ADS1015_REG_CONFIG_MUX_SINGLE_1 = 0x5000;  // Single-ended AIN1
        private const UInt16 ADS1015_REG_CONFIG_MUX_SINGLE_2 = 0x6000;  // Single-ended AIN2
        private const UInt16 ADS1015_REG_CONFIG_MUX_SINGLE_3 = 0x7000;  // Single-ended AIN3

        private const UInt16 ADS1015_REG_CONFIG_PGA_MASK = 0x0E00;
        private const UInt16 ADS1015_REG_CONFIG_PGA_6_144V = 0x0000;  // +/-6.144V range = Gain 2/3
        private const UInt16 ADS1015_REG_CONFIG_PGA_4_096V = 0x0200;  // +/-4.096V range = Gain 1
        private const UInt16 ADS1015_REG_CONFIG_PGA_2_048V = 0x0400;  // +/-2.048V range = Gain 2 (default)
        private const UInt16 ADS1015_REG_CONFIG_PGA_1_024V = 0x0600;  // +/-1.024V range = Gain 4
        private const UInt16 ADS1015_REG_CONFIG_PGA_0_512V = 0x0800;  // +/-0.512V range = Gain 8
        private const UInt16 ADS1015_REG_CONFIG_PGA_0_256V = 0x0A00;  // +/-0.256V range = Gain 16

        private const UInt16 ADS1015_REG_CONFIG_MODE_MASK = 0x0100;
        private const UInt16 ADS1015_REG_CONFIG_MODE_CONTIN = 0x0000;  // Continuous conversion mode
        private const UInt16 ADS1015_REG_CONFIG_MODE_SINGLE = 0x0100;  // Power-down single-shot mode (default)

        private const UInt16 ADS1015_REG_CONFIG_DR_MASK = 0x00E0;
        private const UInt16 ADS1015_REG_CONFIG_DR_128SPS = 0x0000;  // 128 samples per second
        private const UInt16 ADS1015_REG_CONFIG_DR_250SPS = 0x0020;  // 250 samples per second
        private const UInt16 ADS1015_REG_CONFIG_DR_490SPS = 0x0040;  // 490 samples per second
        private const UInt16 ADS1015_REG_CONFIG_DR_920SPS = 0x0060;  // 920 samples per second
        private const UInt16 ADS1015_REG_CONFIG_DR_1600SPS = 0x0080;  // 1600 samples per second (default)
        private const UInt16 ADS1015_REG_CONFIG_DR_2400SPS = 0x00A0;  // 2400 samples per second
        private const UInt16 ADS1015_REG_CONFIG_DR_3300SPS = 0x00C0;  // 3300 samples per second

        private const UInt16 ADS1015_REG_CONFIG_CMODE_MASK = 0x0010;
        private const UInt16 ADS1015_REG_CONFIG_CMODE_TRAD = 0x0000;  // Traditional comparator with hysteresis (default)
        private const UInt16 ADS1015_REG_CONFIG_CMODE_WINDOW = 0x0010;  // Window comparator

        private const UInt16 ADS1015_REG_CONFIG_CPOL_MASK = 0x0008;
        private const UInt16 ADS1015_REG_CONFIG_CPOL_ACTVLOW = 0x0000;  // ALERT/RDY pin is low when active (default)
        private const UInt16 ADS1015_REG_CONFIG_CPOL_ACTVHI = 0x0008;  // ALERT/RDY pin is high when active

        private const UInt16 ADS1015_REG_CONFIG_CLAT_MASK = 0x0004;  // Determines if ALERT/RDY pin latches once asserted
        private const UInt16 ADS1015_REG_CONFIG_CLAT_NONLAT = 0x0000;  // Non-latching comparator (default)
        private const UInt16 ADS1015_REG_CONFIG_CLAT_LATCH = 0x0004;  // Latching comparator

        private const UInt16 ADS1015_REG_CONFIG_CQUE_MASK = 0x0003;
        private const UInt16 ADS1015_REG_CONFIG_CQUE_1CONV = 0x0000;  // Assert ALERT/RDY after one conversions
        private const UInt16 ADS1015_REG_CONFIG_CQUE_2CONV = 0x0001;  // Assert ALERT/RDY after two conversions
        private const UInt16 ADS1015_REG_CONFIG_CQUE_4CONV = 0x0002;  // Assert ALERT/RDY after four conversions
        private const UInt16 ADS1015_REG_CONFIG_CQUE_NONE = 0x0003;  // Disable the comparator and put ALERT/RDY in high state (default)
        /*=========================================================================*/

        enum AdsGain
        {
            GAIN_TWOTHIRDS = ADS1015_REG_CONFIG_PGA_6_144V,
            GAIN_ONE = ADS1015_REG_CONFIG_PGA_4_096V,
            GAIN_TWO = ADS1015_REG_CONFIG_PGA_2_048V,
            GAIN_FOUR = ADS1015_REG_CONFIG_PGA_1_024V,
            GAIN_EIGHT = ADS1015_REG_CONFIG_PGA_0_512V,
            GAIN_SIXTEEN = ADS1015_REG_CONFIG_PGA_0_256V
        }

        I2cDevice device;

        private async Task FindDevices()
        {
            var settings = new I2cConnectionSettings(0x48);
            settings.BusSpeed = I2cBusSpeed.FastMode;
            settings.SharingMode = I2cSharingMode.Shared;

            string i2cDeviceSelector = I2cDevice.GetDeviceSelector();
            IReadOnlyList<DeviceInformation> devices = await DeviceInformation.FindAllAsync(i2cDeviceSelector);
            device = await I2cDevice.FromIdAsync(devices[0].Id, settings);
        }

        public ADC(byte deviceAddress = ADS1015_ADDRESS, UInt16 gain = ADS1015_REG_CONFIG_PGA_2_048V)
        {
            _deviceAddress = deviceAddress;
            _gain = gain;
            _conversionDelay = ADS1015_CONVERSIONDELAY;
            _bitShift = 4;
            FindDevices();
        }

        void writeRegister(byte i2cAddress, byte reg, UInt16 value)
        {
            byte[] cmd = new byte[] { reg, (byte)(value >> 8), (byte)(value & 0xFF) };

            device.Write(cmd);
        }

        UInt16 readRegister(byte i2cAddress)
        {
            byte[] reg = new byte[] { ADS1015_REG_POINTER_CONVERT };

            var HTU21D_settings = new I2cConnectionSettings(0x40);
            byte[] res = new byte[2];
            device.WriteRead(reg, res);

            return (UInt16)((res[0] << 8) | res[1]);
        }


        UInt16 readADC_SingleEnded(byte channel)
        {
            if (channel > 3)
            {
                return 0;
            }

            // Start with default values
            UInt16 config = ADS1015_REG_CONFIG_CQUE_NONE | // Disable the comparator (default val)
                            ADS1015_REG_CONFIG_CLAT_NONLAT | // Non-latching (default val)
                            ADS1015_REG_CONFIG_CPOL_ACTVLOW | // Alert/Rdy active low   (default val)
                            ADS1015_REG_CONFIG_CMODE_TRAD | // Traditional comparator (default val)
                            ADS1015_REG_CONFIG_DR_1600SPS | // 1600 samples per second (default)
                            ADS1015_REG_CONFIG_MODE_SINGLE;   // Single-shot mode (default)

            // Set PGA/voltage range
            config |= _gain;

            // Set single-ended input channel
            switch (channel)
            {
                case (0):
                    config |= ADS1015_REG_CONFIG_MUX_SINGLE_0;
                    break;
                case (1):
                    config |= ADS1015_REG_CONFIG_MUX_SINGLE_1;
                    break;
                case (2):
                    config |= ADS1015_REG_CONFIG_MUX_SINGLE_2;
                    break;
                case (3):
                    config |= ADS1015_REG_CONFIG_MUX_SINGLE_3;
                    break;
            }

            // Set 'start single-conversion' bit
            config |= ADS1015_REG_CONFIG_OS_SINGLE;

            // Write config register to the ADC
            writeRegister(_deviceAddress, ADS1015_REG_POINTER_CONFIG, config);

            // Wait for the conversion to complete
            Thread.Sleep(_conversionDelay);

            // Read the conversion results
            // Shift 12-bit results right 4 bits for the ADS1015
            return (UInt16)(readRegister(_deviceAddress) >> _bitShift);
        }
        
        public Int16 readADC_Differential_0_1()
        {
            // Start with default values
            UInt16 config = ADS1015_REG_CONFIG_CQUE_NONE | // Disable the comparator (default val)
                            ADS1015_REG_CONFIG_CLAT_NONLAT | // Non-latching (default val)
                            ADS1015_REG_CONFIG_CPOL_ACTVLOW | // Alert/Rdy active low   (default val)
                            ADS1015_REG_CONFIG_CMODE_TRAD | // Traditional comparator (default val)
                            ADS1015_REG_CONFIG_DR_1600SPS | // 1600 samples per second (default)
                            ADS1015_REG_CONFIG_MODE_SINGLE;   // Single-shot mode (default)

            // Set PGA/voltage range
            config |= _gain;

            // Set channels
            config |= ADS1015_REG_CONFIG_MUX_DIFF_0_1;          // AIN0 = P, AIN1 = N

            // Set 'start single-conversion' bit
            config |= ADS1015_REG_CONFIG_OS_SINGLE;

            // Write config register to the ADC
            writeRegister(_deviceAddress, ADS1015_REG_POINTER_CONFIG, config);

            // Wait for the conversion to complete
            Thread.Sleep(_conversionDelay);

            // Read the conversion results
            UInt16 res = (UInt16)(readRegister(_deviceAddress) >> _bitShift);
            if (_bitShift == 0)
            {
                return (Int16)res;
            }
            else
            {
                // Shift 12-bit results right 4 bits for the ADS1015,
                // making sure we keep the sign bit intact
                if (res > 0x07FF)
                {
                    // negative number - extend the sign to 16th bit
                    res |= 0xF000;
                }
                return (Int16)res;
            }
        }

        Int16 readADC_Differential_2_3()
        {
            // Start with default values
            UInt16 config = ADS1015_REG_CONFIG_CQUE_NONE | // Disable the comparator (default val)
                            ADS1015_REG_CONFIG_CLAT_NONLAT | // Non-latching (default val)
                            ADS1015_REG_CONFIG_CPOL_ACTVLOW | // Alert/Rdy active low   (default val)
                            ADS1015_REG_CONFIG_CMODE_TRAD | // Traditional comparator (default val)
                            ADS1015_REG_CONFIG_DR_1600SPS | // 1600 samples per second (default)
                            ADS1015_REG_CONFIG_MODE_SINGLE;   // Single-shot mode (default)

            // Set PGA/voltage range
            config |= _gain;

            // Set channels
            config |= ADS1015_REG_CONFIG_MUX_DIFF_2_3;          // AIN2 = P, AIN3 = N

            // Set 'start single-conversion' bit
            config |= ADS1015_REG_CONFIG_OS_SINGLE;

            // Write config register to the ADC
            writeRegister(_deviceAddress, ADS1015_REG_POINTER_CONFIG, config);

            // Wait for the conversion to complete
            Thread.Sleep(_conversionDelay);

            // Read the conversion results
            UInt16 res = (UInt16)(readRegister(_deviceAddress) >> _bitShift);
            if (_bitShift == 0)
            {
                return (Int16)res;
            }
            else
            {
                // Shift 12-bit results right 4 bits for the ADS1015,
                // making sure we keep the sign bit intact
                if (res > 0x07FF)
                {
                    // negative number - extend the sign to 16th bit
                    res |= 0xF000;
                }
                return (Int16)res;
            }
        }
    }
}
