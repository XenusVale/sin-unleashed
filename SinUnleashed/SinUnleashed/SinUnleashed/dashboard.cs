/*
 * "To the dreams of my childhood ~
 *                                  Farewell"
 *                                  
 * XenusVale presents:
 * 
 *      Final Fantasy X: Sin Unleashed
 */

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace SinUnleashed
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void SinUnleashed_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        #region "Global Variable Declaration"

        public static int APHold = 0;

        public static int ffxEntryPointAddress = 0x00;

        public static int kimaSpherePosition = 0;

        public static int nopeCodeProtector = 0;

        public static bool cOrN = false;

        public static bool firstPass = true;

        public static bool FFXIsOn = false;

        public static bool gameHasBeenOn = false;

        public static bool seymourModHasBeenOn = false;

        public static bool weHaveBeenInCombat = false;

        public static bool aeonListAffected = false;

        public static bool nopCodeInjected = false;

        public static bool nopeCodeInjected = false;

        public static bool noperCodeInjected = false;

        public static bool augmentInjected = false;

        public static bool equipAugmentInjected = false;

        public static bool skipThisLoop = false;

        public static bool viewOrSetMode = false;

        public static bool photoBomb = false;

        public static bool kGearRemoved = false;

        public static bool yGearAdded = false;

        public static bool shopReset = false;

        public static bool shopJustOnce = false;

        public static bool inCombatAugmentInjected = false;

        public static bool celeWepOnce = false;

        public static bool saveSpherePictureEditted = true;

        public static bool removeAnimaNow = false;

        public static bool painWasRemoved = false;

        public static bool fakeAnimaUnlock = false;

        public static byte animaIsActuallyUnlocked = 0x10;

        public static bool[] justOnce = { false, false, false, false };

        public static bool[] equipPhotoRemoved = { false, false };

        public static bool[] swappedGear = { false, false, false, false };

        public static bool[] combatGearReset = { false, false };

        public static int[] APGain = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public static byte[] yAeonsAvailable = new byte[20];

        public static byte[] sAeonsAvailable = new byte[20];

        public static byte[] ronsoRageMenuAssemblyBytesControl2 = new byte[5];

        public static byte[] ronsoRageMenuAssemblyBytesControl3 = new byte[5];

        public static byte[] ronsoRageMenuAssemblyBytes = new byte[5];

        public static byte[] ronsoRageMenuAssemblyBytes2 = new byte[5];

        public static byte[] whatDidYouHave = new byte[22];

        public static byte[] yCombatGearArrayWepConstant = new byte[22];

        public static byte[] yCombatGearArrayArmConstant = new byte[22];

        public static byte[] kCombatGearArrayWepConstant = new byte[22];

        public static byte[] kCombatGearArrayArmConstant = new byte[22];

        #endregion

        private void SeymoursPathCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ((seymoursPathCheckBox.Checked == true) && (Process.GetProcessesByName("FFX").Length == 0))
            {
                MessageBox.Show("Please start Final Fantasy X before selecting mods.");
                seymoursPathCheckBox.Checked = false;
            }
        }

        private void ChimeraModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (chimeraModeCheckBox.Checked == true)
            {
                nightmareModeCheckBox.Checked = false;

                if (Process.GetProcessesByName("FFX").Length == 0)
                {
                    MessageBox.Show("Please start Final Fantasy X before selecting mods.");
                    chimeraModeCheckBox.Checked = false;
                }
            }
        }

        private void NightmareModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (nightmareModeCheckBox.Checked == true)
            {
                chimeraModeCheckBox.Checked = false;

                if (Process.GetProcessesByName("FFX").Length == 0)
                {
                    MessageBox.Show("Please start Final Fantasy X before selecting mods.");
                    nightmareModeCheckBox.Checked = false;
                }
            }
        }

        private void RunMods_Tick(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("FFX").Length > 0)
            {
                FFXIsOn = true;
                gameHasBeenOn = true;
                Process[] ffxProcess = Process.GetProcessesByName("FFX");

                try
                {
                    foreach (ProcessModule ffxProcessModule in ffxProcess[0].Modules)
                    {
                        if (ffxProcessModule.ModuleName.Equals("FFX.exe"))
                        {
                            Dashboard.ffxEntryPointAddress = (int)(ffxProcessModule.BaseAddress);
                        }
                    }
                }
                catch
                {
                    Environment.Exit(0);
                }
            }
            else if ((Process.GetProcessesByName("FFX").Length == 0) && (gameHasBeenOn == false))
            {
                FFXIsOn = false;
            }
            else
            {
                Environment.Exit(0);
            }

            if ((seymoursPathCheckBox.Checked == true) && (FFXIsOn))
            {
                SinUnleashed sinUn = new SinUnleashed();
                sinUn.SeymourModOn();
            }
            else if ((seymoursPathCheckBox.Checked == false) && (FFXIsOn))
            {
                SinUnleashed sinUn = new SinUnleashed();
                sinUn.SeymourModOff();
            }

            if ((chimeraModeCheckBox.Checked == true) && (FFXIsOn))
            {
                cOrN = true;
                SinUnleashed sin = new SinUnleashed();
                sin.DifficultyMod();
            }
            else if ((nightmareModeCheckBox.Checked == true) && (FFXIsOn))
            {

                cOrN = false;
                SinUnleashed sin = new SinUnleashed();
                sin.DifficultyMod();
            }
        }

    }

    class Memory
    {
        [DllImport("kernel32.dll")]
        public static extern int OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(IntPtr hProcess);

        public static Process ffx = Process.GetProcessesByName("FFX").FirstOrDefault();

        public static uint Initialization()
        {
            uint delete = 0x00010000;
            uint read = 0x00020000;
            uint writeDAC = 0x00040000;
            uint writeOwner = 0x00080000;
            uint sync = 0x00100000;
            uint end = 0xFFF;
            uint fullControl = (delete | read | writeDAC | writeOwner | sync | end);

            return fullControl;
        }

        public static byte ReadByte(IntPtr address)
        {
            uint fullControl = Initialization();

            IntPtr authorization = (IntPtr)OpenProcess(fullControl, false, Memory.ffx.Id);

            byte[] bytesToBeReadBuffer = new byte[1];
            ReadProcessMemory(authorization, address, bytesToBeReadBuffer, 1, 0);
            byte byteToReturn = bytesToBeReadBuffer[0];

            CloseHandle(authorization);

            return byteToReturn;
        }

        public static int ReadInt32(IntPtr address)
        {
            uint fullControl = Initialization();

            IntPtr authorization = (IntPtr)OpenProcess(fullControl, false, Memory.ffx.Id);

            byte[] bytesToBeReadBuffer = new byte[4];
            ReadProcessMemory(authorization, address, bytesToBeReadBuffer, bytesToBeReadBuffer.Length, 0);

            int valueToBeReturned = BitConverter.ToInt32(bytesToBeReadBuffer, 0);

            CloseHandle(authorization);

            return valueToBeReturned;
        }

        public static byte[] ReadByteArray(IntPtr address, int numOfBytesToBeRead)
        {
            uint fullControl = Initialization();

            IntPtr authorization = (IntPtr)OpenProcess(fullControl, false, Memory.ffx.Id);

            byte[] bytesToBeReadBuffer = new byte[numOfBytesToBeRead];
            ReadProcessMemory(authorization, address, bytesToBeReadBuffer, numOfBytesToBeRead, 0);

            CloseHandle(authorization);

            return bytesToBeReadBuffer;
        }

        public static void WriteByte(IntPtr address, byte byteToBeWritten)
        {
            uint fullControl = Initialization();

            IntPtr authorization = (IntPtr)OpenProcess(fullControl, false, Memory.ffx.Id);

            byte[] bytesToBeWritten = new byte[1];
            bytesToBeWritten[0] = byteToBeWritten;
            WriteProcessMemory(authorization, address, bytesToBeWritten, bytesToBeWritten.Length, 0);

            CloseHandle(authorization);
        }

        public static void WriteInt32(IntPtr address, int valueToBeWritten)
        {
            uint fullControl = Initialization();

            IntPtr authorization = (IntPtr)OpenProcess(fullControl, false, Memory.ffx.Id);

            byte[] bytesToBeWritten = BitConverter.GetBytes(valueToBeWritten);
            WriteProcessMemory(authorization, address, bytesToBeWritten, bytesToBeWritten.Length, 0);

            CloseHandle(authorization);
        }

        public static void WriteByteArray(IntPtr address, byte[] bytesToBeWritten)
        {
            uint fullControl = Initialization();

            IntPtr authorization = (IntPtr)OpenProcess(fullControl, false, Memory.ffx.Id);

            WriteProcessMemory(authorization, address, bytesToBeWritten, bytesToBeWritten.Length, 0);

            CloseHandle(authorization);
        }
    }

    public class Functions
    {
        static public void StatChange(int enemyByteAddress, int cHP, int cMP, int mHP, int mMP, int stats)
        {
            bool chiOrNi = true;
            double statsMultiplier = 1.0;
            byte[] allTheStats = new byte[8];
            int[] hotfixUnbeatable = new int[12];
            int[] hotfixRuinsBoss = { 1500, 5, 1500, 5, 14, 1, 1, 1, 4, 15, 0, 50 };
            byte[] ruinsBossRework = { 0x11, 0x02, 0x02, 0x02, 0x06, 0x0F, 0x00, 0x32 };
            int[] hotfixKimahriBoss = { 750, 10, 750, 10, 10, 15, 8, 5, 15, 15, 0, 10 };
            byte[] kimahriBossRework = { 0x0C, 0x11, 0x0A, 0x0A, 0x0F, 0x0F, 0x00, 0x0A };
            int[] hotfixSinSpawnBoss = { 2000, 20, 2000, 20, 10, 1, 15, 1, 5, 15, 0, 15 };
            byte[] sinSpawnBossRework = { 0x0F, 0x02, 0x11, 0x02, 0x07, 0x0F, 0x00, 0x0F };


            chiOrNi = Dashboard.cOrN;

            if (chiOrNi == true)
            {
                statsMultiplier = 1.3;
            }
            else if (chiOrNi == false)
            {
                statsMultiplier = 1.6;
            }

            int enemyDifficulty = Memory.ReadInt32((IntPtr)enemyByteAddress);

            int finalAddress = enemyDifficulty + cHP;
            int originalValue = Memory.ReadInt32((IntPtr)finalAddress);
            hotfixUnbeatable[0] = originalValue;
            int newValue = (int)(originalValue * statsMultiplier);
            Memory.WriteInt32((IntPtr)finalAddress, newValue);

            finalAddress = enemyDifficulty + cMP;
            originalValue = Memory.ReadInt32((IntPtr)finalAddress);
            hotfixUnbeatable[1] = originalValue;
            newValue = (int)(originalValue * statsMultiplier);
            Memory.WriteInt32((IntPtr)finalAddress, newValue);

            finalAddress = enemyDifficulty + mHP;
            originalValue = Memory.ReadInt32((IntPtr)finalAddress);
            hotfixUnbeatable[2] = originalValue;
            newValue = (int)(originalValue * statsMultiplier);
            Memory.WriteInt32((IntPtr)finalAddress, newValue);

            finalAddress = enemyDifficulty + mMP;
            originalValue = Memory.ReadInt32((IntPtr)finalAddress);
            hotfixUnbeatable[3] = originalValue;
            newValue = (int)(originalValue * statsMultiplier);
            Memory.WriteInt32((IntPtr)finalAddress, newValue);

            finalAddress = enemyDifficulty + stats;
            allTheStats = Memory.ReadByteArray((IntPtr)finalAddress, 8);

            int n = 4;
            for (int i = 0; i < 8; i++)
            {
                hotfixUnbeatable[n] = allTheStats[i];
                n++;
            }

            for (int i = 0; i < 6; i++)
            {
                if ((allTheStats[i] >= 196) && (statsMultiplier == 1.3))
                {
                    allTheStats[i] = 255;
                }
                else if ((allTheStats[i] >= 159) && (statsMultiplier == 1.6))
                {
                    allTheStats[i] = 255;
                }
                else
                {
                    allTheStats[i] = (byte)(allTheStats[i] * statsMultiplier);
                }
            }

            if ((allTheStats[7] >= 196) && (statsMultiplier == 1.3))
            {
                allTheStats[7] = 255;
            }
            else if ((allTheStats[7] >= 159) && (statsMultiplier == 1.6))
            {
                allTheStats[7] = 255;
            }
            else
            {
                allTheStats[7] = (byte)(allTheStats[7] * statsMultiplier);
            }
            Memory.WriteByteArray((IntPtr)finalAddress, allTheStats);

            #region "Unbeatable Boss Hotfixes"

            if (Enumerable.SequenceEqual(hotfixUnbeatable, hotfixRuinsBoss))
            {
                Memory.WriteInt32((IntPtr)(enemyDifficulty + cHP), 2200);
                Memory.WriteInt32((IntPtr)(enemyDifficulty + mHP), 2200);
                Memory.WriteByteArray((IntPtr)finalAddress, ruinsBossRework);
            }
            else if (Enumerable.SequenceEqual(hotfixUnbeatable, hotfixKimahriBoss))
            {
                Memory.WriteInt32((IntPtr)(enemyDifficulty + cHP), 850);
                Memory.WriteInt32((IntPtr)(enemyDifficulty + mHP), 850);
                Memory.WriteByteArray((IntPtr)finalAddress, kimahriBossRework);
            }
            else if (Enumerable.SequenceEqual(hotfixUnbeatable, hotfixSinSpawnBoss))
            {
                Memory.WriteInt32((IntPtr)(enemyDifficulty + cHP), 1000);
                Memory.WriteInt32((IntPtr)(enemyDifficulty + mHP), 1000);
                Memory.WriteByteArray((IntPtr)finalAddress, sinSpawnBossRework);
            }

            #endregion
        }

        static public int Offsets(int offsetCaller)
        {
            int[] offsetArray = new int[]
            {
                0x000000, 0xD32494, 0xD307FD, 0xD324A3, 0x85A780,
                0xD3247C, 0xD32480, 0xD32484, 0xD32488, 0xD3248C,
                0xD32490, 0xD32474, 0xD324A4, 0xD324A8, 0xD324AC,
                0xD324B0, 0xD324F0, 0xD324A0, 0xD32244, 0x1FCC088,
                0xD32E18, 0xD32253, 0x4C23B7, 0xD3222C, 0xD32230,
                0xD32234, 0xD32238, 0xD3223C, 0xD32240, 0xD32247,
                0xD32254, 0xD32258, 0xD3225C, 0xD32260, 0xD322A0,
                0xD32250, 0xD34460, 0xF3C954, 0x12BEA2C, 0xD324C8,
                0xD32278, 0xD32298, 0xD324E8, 0xF3C930, 0xD307E8,
                0xD3280C, 0x85A7F0, 0xD2C268, 0xD324B8, 0xD2A92C,
                0x3855CA, 0x1197730, 0x4C23C6, 0x1440A38, 0xD3281C,
                0x1441BE8, 0x1440AB8, 0x4C23D1, 0x1440A35, 0xD3352B,
                0x4D057A, 0x1440B50, 0x64622B, 0x85B30C, 0x8CB9D0,
                0x6462D0, 0x1FCC3C4, 0x658F18, 0xD30F2C, 0xD3155C,
                0x1440B65, 0x85A75C, 0x4E6813, 0x3ABE2C, 0xD32245,
                0xD32495, 0x85A838, 0xD32DF0, 0xD3211D, 0xD2CDD0,
                0xD30F1C, 0xD30F21, 0x1465CDE, 0x85A738, 0xD32070,
                0x4A02CE, 0x4D4FCD, 0x3ABE2C, 0x146A33F, 0x1471508,
                0x4BF773, 0xD363A4, 0xF00704, 0x85A814, 0x84949C,
                0x8E7300
            };

            int kbaseByteAddress = (Dashboard.ffxEntryPointAddress + (offsetArray[offsetCaller]));
            return kbaseByteAddress;
        }
    }

    class SinUnleashed
    {
        public void DifficultyMod()
        {
            int baseByteAddressEnemy;
            int APGainByteAddress;
            int basecHP = 0x5D0;
            int basecMP = 0x5D4;
            int basemHP = 0x594;
            int basemMP = 0x598;
            int baseStats = 0x5A8;
            int weGainedAPHolder = 0;
            int[] APGainHolder = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double APMultiplier = 0.0;
            bool chimeOrNight = true;

            baseByteAddressEnemy = (Dashboard.ffxEntryPointAddress + 0xD34460);
            APGainByteAddress = (Dashboard.ffxEntryPointAddress + 0x1F10F20);

            int combatIdentifier = Memory.ReadInt32((IntPtr)baseByteAddressEnemy);
            int combatIdentifierDelayPointer = Memory.ReadInt32((IntPtr)(baseByteAddressEnemy));
            int combatIdentifierDelayFinal = Memory.ReadInt32((IntPtr)(combatIdentifierDelayPointer + 0x5D0));

            if (combatIdentifier == 0)
            {
                Dashboard.firstPass = true;
            }

            if ((combatIdentifier > 0) && (combatIdentifierDelayFinal > 0) && (Dashboard.firstPass == true))
            {
                for (int i = 0; i < 8; i++)
                {
                    Functions.StatChange(baseByteAddressEnemy, basecHP, basecMP, basemHP, basemMP, baseStats);
                    basecHP = basecHP + 0xF90;
                    basecMP = basecMP + 0xF90;
                    basemHP = basemHP + 0xF90;
                    basemMP = basemMP + 0xF90;
                    baseStats = baseStats + 0xF90;
                }
                Dashboard.firstPass = false;
            }

            weGainedAPHolder = Dashboard.APHold;

            int weGainedAP = Memory.ReadInt32((IntPtr)APGainByteAddress);

            if (weGainedAP > weGainedAPHolder)
            {
                chimeOrNight = Dashboard.cOrN;

                if (chimeOrNight == true)
                {
                    APMultiplier = 0.9;
                }
                else if (chimeOrNight == false)
                {
                    APMultiplier = 0.8;
                }

                for (int i = 0; i < 8; i++)
                {
                    APGainHolder[i] = Dashboard.APGain[i];
                }

                for (int n = 0; n < 8; n++)
                {
                    int APGain = Memory.ReadInt32((IntPtr)APGainByteAddress);

                    if (APGain > APGainHolder[n])
                    {
                        APGain = (int)(APGainHolder[n] + ((APGain - APGainHolder[n]) * APMultiplier));
                    }

                    Memory.WriteInt32((IntPtr)APGainByteAddress, APGain);
                    APGainHolder[n] = APGain;
                    APGainByteAddress = (APGainByteAddress + 0x04);
                }

                APGainByteAddress = (Dashboard.ffxEntryPointAddress + 0x1F10F20);
                Dashboard.APHold = Memory.ReadInt32((IntPtr)APGainByteAddress);

                for (int i = 0; i < 8; i++)
                {
                    Dashboard.APGain[i] = APGainHolder[i];
                }
            }
        }

        public void SeymourModOn()
        {
            Dashboard.seymourModHasBeenOn = true;

            #region "Array Declaration"

            byte[] kNametoSName =
            {
                0x62, 0x74, 0x88, 0x7C, 0x7E, 0x84, 0x81
            };
            byte[] kNameReset =
            {
                0x5A, 0x78, 0x7C, 0x70, 0x77, 0x81, 0x78
            };
            byte[] yNametoSYName =
            {
                0x68, 0x84, 0x7D, 0x70, 0x3A, 0x40, 0x3A, 0x62, 0x74, 0x88,
                0x7C, 0x7E, 0x84, 0x81, 0x00
            };
            byte[] yNameReset =
            {
                0x68, 0x84, 0x7D, 0x70, 0x00
            };
            byte[] staffNameReset =
            {
                0x62, 0x83, 0x70, 0x75, 0x75, 0x00
            };
            byte[] staffToSName =
            {
                0x61, 0x74, 0x73, 0x74, 0x7C, 0x7F, 0x83, 0x78, 0x7E, 0x7D,
                0x00
            };
            byte[] ronsoRageNewName =
            {
                0x61, 0x74, 0x80, 0x84, 0x78, 0x74, 0x7C, 0xFF, 0xFF, 0xFF,
                0x00, 0x47, 0x00, 0x60, 0x84, 0x78, 0x74, 0x83, 0x3A, 0x88,
                0x7E, 0x84, 0x81, 0x3A, 0x74, 0x7D, 0x74, 0x7C, 0x78, 0x74,
                0x82, 0x3A, 0x82, 0x7E, 0x84, 0x7B, 0x82, 0x48, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF
            };
            byte[] kOverdriveMenuEdit =
            {
                0x97, 0x30, 0x01, 0x01, 0x9C, 0x30, 0x01, 0x01, 0xA0, 0x30,
                0x01, 0x01, 0xA6, 0x30, 0x01, 0x01, 0x98, 0x30, 0x01, 0x01,
                0x9E, 0x30, 0x01, 0x01, 0xA3, 0x30, 0x01, 0x01, 0xA8, 0x30,
                0x01, 0x01, 0x67, 0x40, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
            byte[] nopCode =
            {
                0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90
            };
            byte[] nopeCode =
            {
                0x90, 0x90, 0x90
            };
            byte[] noperCode =
            {
                0x90, 0x90, 0x90, 0x90
            };
            byte[] nopetCode =
            {
                0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90
            };
            byte[] nopitCode =
            {
                0x90, 0x90, 0x90, 0x90, 0x90
            };
            byte[] nopyCode =
            {
                0x90, 0x90
            };
            byte[] gridPhotoAssemblyBytes =
            {
                0x0F, 0xB6, 0x81, 0xC5, 0x15, 0x01, 0x00
            };
            byte[] gridPhotoEffectAssemblyBytes =
            {
                0x0F, 0xB6, 0x82, 0xC5, 0x15, 0x01, 0x00
            };
            byte[] gridPhotoAssemblyBytesControl =
            {
                0x0F, 0xB6, 0x81, 0xBC, 0x15, 0x01, 0x00
            };
            byte[] gridPhotoEffectAssemblyBytesControl =
            {
                0x0F, 0xB6, 0x82, 0xBC, 0x15, 0x01, 0x00
            };
            byte[] combatAssemblyBytes =
            {
                0x66, 0x89, 0x34, 0x7D, 0x68, 0xC2, 0x5A, 0x01
            };
            byte[] ronsoRageMenuAssemblyBytesControl1 =
            {
                0x89, 0x41, 0xFD
            };
            byte[] ronsoRageMenuAssemblyBytesControl4 =
            {
                0x66, 0x89, 0x41, 0x30
            };
            byte[] forceOutOfViewControl =
            {
                0x97, 0x30
            };
            byte[] numberOfOverdrivesToShowBytesControl =
            {
                0x00, 0x50
            };
            byte[] sOverdriveOptions =
            {
                0x97, 0x30, 0x9C, 0x30, 0xA0, 0x30, 0xA6, 0x30, 0x98, 0x30,
                0x9E, 0x30, 0xA3, 0x30, 0xA8, 0x30, 0x67, 0x40
            };
            byte[] sOverdriveGeneralEdit =
            {
                0x1A, 0x31
            };
            byte[] switchToSummon =
            {
                0x00, 0x30, 0x17, 0x31
            };
            byte[] kSphereLevel =
            {
                0x00
            };
            byte[] sSphereLevel =
            {
                0x00
            };
            byte[] gearArray = new byte[22];
            byte[] gearArrayShatter1 = new byte[5];
            byte[] gearArrayShatter2 = new byte[15];
            byte[] combatGearArray =
            {
                0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00,
                0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00,
                0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00
            };
            byte[] emptyGearArray =
            {
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00,
            };
            byte[] iconAssemblyCode =
            {
                0x8A, 0x4F, 0x04
            };
            byte[] equipNameAssemblyCode =
            {
                0x0F, 0xB6, 0x48, 0x04
            };
            byte[] kInPartyArray =
            {
                0x11
            };
            byte[] kChangeEquipped =
            {
                0xFF, 0xFF
            };
            byte[] yChangeEquipped =
            {
                0xFF, 0xFF
            };
            byte[] kStartingWep =
            {
                0x01, 0x50, 0x01, 0x00, 0x03, 0x00, 0x03, 0x00, 0x01, 0x0F,
                0x03, 0x00, 0x1F, 0x40, 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00,
                0xFF, 0x00
            };
            byte[] kStartingArm =
            {
                0x6E, 0x50, 0x01, 0x00, 0x03, 0x01, 0x03, 0x00, 0x01, 0x0F,
                0x03, 0x00, 0x50, 0x40, 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00,
                0xFF, 0x00
            };
            byte[] kStartingSet =
            {
                0x60, 0x61
            };
            byte[] sCelestialWeaponShatter1 =
            {
                0x01, 0x50, 0x01, 0x00
            };
            byte[] sCelestialWeaponShatter2 =
            {
                0x00, 0x12, 0x10, 0x03, 0x04, 0x66, 0x40, 0x19, 0x80, 0x0F,
                0x80, 0x06, 0x80, 0x0D, 0x80
            };
            byte[] saveSpherePictureReset =
            {
                0x03
            };
            byte[] readPartyFormation =
            {
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
            byte[] aeonArray =
            {
                0x10
            };
            byte[] ffArray =
            {
                0xFF
            };
            byte[] checkIfModHasBeenOn = new byte[7];
            byte[] equipPictureRemovalConstant =
            {
                0xE8, 0xFE, 0xF9, 0xD6, 0xFF
            };
            byte[] inCombatIconAssemblyBytesControl =
            {
                0x0F, 0xB6, 0x46, 0x04
            };
            byte[] inCombatNameAssemblyBytesControl =
            {
                0x0F, 0xB6, 0x48, 0x04
            };
            byte[] equipPictureEffectRemovalConstant =
            {
                0x3B, 0xCE
            };
            byte[] yHasNoWeapon =
            {
                0x03, 0x50, 0x01, 0x00, 0x03, 0x00, 0x03, 0x00, 0x01, 0x0F,
                0x03, 0x00, 0x0B, 0x40, 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00,
                0xFF, 0x00
            };
            byte[] writePartyFormation =
            {
                0x05, 0x00, 0x03, 0x01, 0x04, 0xFF, 0xFF, 0xFF
            };
            int[] sinHotfixConstant =
            {
                2000, 100, 2000, 100, 1, 1, 1, 1, 6, 15,
                0, 1
            };
            int[] sinHotfixConstant2 =
            {
                3200, 180, 3200, 180, 1, 1, 1, 1, 10, 27,
                0, 1
            };
            int[] sinHotfixConstant3 =
            {
                3000, 150, 3000, 150, 1, 1, 1, 1, 9, 22,
                0, 1
            };
            int[] sinGenHotfixConstant =
            {
                3000, 30, 3000, 30, 15, 1, 10, 1, 7, 1,
                0, 100
            };
            int[] sinGenHotfixConstant2 =
            {
                5400, 54, 5400, 54, 27, 1, 18, 1, 12, 1,
                0, 180
            };
            int[] sinGenHotfixConstant3 =
            {
                4500, 45, 4500, 45, 22, 1, 15, 1, 10, 1,
                0, 150
            };
            int[] sinHotfix = new int[12];
            bool[] celestialWeapon =
            {
                false, false, false, false, false
            };

            #endregion

            #region "Check if Mod Has Been On"

            int sTotalFights = Memory.ReadInt32((IntPtr)Functions.Offsets(48));
            if (sTotalFights > 1)
            {
                Dashboard.kGearRemoved = true;
                Dashboard.justOnce[1] = true;
                Dashboard.justOnce[2] = true;
            }

            #endregion

            #region "Initialization of Seymour"

            int storyProgress = Memory.ReadInt32((IntPtr)Functions.Offsets(94));
            int kTotalAP = Memory.ReadInt32((IntPtr)(Functions.Offsets(23)));
            int kCurrentHP = Memory.ReadInt32((IntPtr)(Functions.Offsets(25)));
            int kSpherePosition = Memory.ReadByte((IntPtr)(Functions.Offsets(38)));
            int sTotalAP = Memory.ReadInt32((IntPtr)(Functions.Offsets(5)));
            int sCurrentAP = Memory.ReadInt32((IntPtr)(Functions.Offsets(6)));
            int numberOfAeonsInParty = Memory.ReadByte((IntPtr)(Functions.Offsets(48)));
            int kInParty = Memory.ReadByte((IntPtr)(Functions.Offsets(18)));
            int shopIsOpen = Memory.ReadByte((IntPtr)(Functions.Offsets(76)));
            int itemsMenuOpen = Memory.ReadByte((IntPtr)(Functions.Offsets(88)));
            int customizeMenuOpen = Memory.ReadByte((IntPtr)(Functions.Offsets(93)));
            int undoItemsAction = Memory.ReadByte((IntPtr)(Functions.Offsets(70)));
            int menuIsOpen = Memory.ReadByte((IntPtr)(Functions.Offsets(64)));
            int combatIdentifier = Memory.ReadInt32((IntPtr)(Functions.Offsets(36)));
            int tidusTotalAP = Memory.ReadInt32((IntPtr)Functions.Offsets(84));
            if ((combatIdentifier == 0) && (Dashboard.fakeAnimaUnlock == false))
            {
                Dashboard.animaIsActuallyUnlocked = Memory.ReadByte((IntPtr)(Functions.Offsets(45)));
            }
            int animaUnlocked = Dashboard.animaIsActuallyUnlocked;
            kSphereLevel[0] = Memory.ReadByte((IntPtr)(Functions.Offsets(21)));

                if ((shopIsOpen == 0) && (itemsMenuOpen == 0) && (customizeMenuOpen == 0))
                {
                    if ((tidusTotalAP != 0) && (kInParty == 0x11))
                    {
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(20)), kNametoSName);
                    }
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(77)), yNameReset);
                }
                if ((shopIsOpen == 1) || ((itemsMenuOpen == 1) && (menuIsOpen == 1)) || (customizeMenuOpen == 1))
                {
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(20)), kNameReset);
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(77)), yNametoSYName);
                }

                if (Dashboard.justOnce[2] == false)
                {
                    int kCurrentAP = Memory.ReadInt32((IntPtr)(Functions.Offsets(24)));

                    Memory.WriteInt32((IntPtr)(Functions.Offsets(5)), kTotalAP);
                    Memory.WriteInt32((IntPtr)(Functions.Offsets(6)), kCurrentAP);
                    Memory.WriteByte((IntPtr)(Functions.Offsets(3)), kSphereLevel[0]);

                    int kOverdrivesAvailableHolder = Memory.ReadInt32((IntPtr)(Functions.Offsets(34)));
                    Memory.WriteInt32((IntPtr)(Functions.Offsets(16)), kOverdrivesAvailableHolder);

                    for (int i = 0; i < 8; i++)
                    {
                        int kOverdriveUnlock = Memory.ReadInt32((IntPtr)(Functions.Offsets(40) + (0x04 * i)));
                        Memory.WriteInt32((IntPtr)(Functions.Offsets(39) + (0x04 * i)), kOverdriveUnlock);
                    }

                    byte kOverdriveUnlockLoner = Memory.ReadByte((IntPtr)(Functions.Offsets(41)));
                    Memory.WriteByte((IntPtr)(Functions.Offsets(42)), kOverdriveUnlockLoner);

                    if (animaUnlocked != 0x11)
                    {
                        Memory.WriteInt32((IntPtr)(Functions.Offsets(54)), 0);
                        Memory.WriteInt32((IntPtr)(Functions.Offsets(54) + 0x04), 0x08000000);
                        Memory.WriteInt32((IntPtr)(Functions.Offsets(54) + 0x08), 0x001E0000);
                    }

                    Dashboard.justOnce[2] = true;
                }

            #endregion

            if ((kInParty == 0x11) || (kInParty == 0x10))
            {

                #region "Save Sphere Photo Edit"

                int saveSphereOpen = Memory.ReadByte((IntPtr)(Functions.Offsets(82)));
                int saveSphereStillOpen = Memory.ReadByte((IntPtr)(Functions.Offsets(92)));
                int saveSphereStillOpenCloser = Memory.ReadByte((IntPtr)(Functions.Offsets(95)));
                readPartyFormation = Memory.ReadByteArray((IntPtr)((Functions.Offsets(44))), 8);

                if ((saveSphereOpen == 0x01) || (saveSphereStillOpen == 0x01))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (readPartyFormation[i] == 0x03)
                        {
                            Memory.WriteByteArray((IntPtr)(Functions.Offsets(44) + i), ffArray);
                            Dashboard.saveSpherePictureEditted = true;
                        }
                    }
                }
                else if ((saveSphereOpen == 0x00) && (saveSphereStillOpenCloser == 0x00) && (Dashboard.saveSpherePictureEditted == true))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if ((readPartyFormation[i] == 0xFF) && (readPartyFormation[(i + 1)] != 0xFF))
                        {
                            Memory.WriteByteArray((IntPtr)(Functions.Offsets(44) + i), saveSpherePictureReset);
                            Dashboard.saveSpherePictureEditted = false;
                        }
                    }
                }

                #endregion

                #region "Map Seymour Onto Kimahri and Vice Versa"

                Memory.WriteByteArray((IntPtr)(Functions.Offsets(1)), kInPartyArray);

                sSphereLevel[0] = Memory.ReadByte((IntPtr)(Functions.Offsets(3)));

                if ((kSphereLevel[0] < sSphereLevel[0]) && (kSpherePosition == Dashboard.kimaSpherePosition))
                {
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(21)), sSphereLevel);
                }
                else if ((kSphereLevel[0] < sSphereLevel[0]) && (kSpherePosition != Dashboard.kimaSpherePosition))
                {
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(3)), kSphereLevel);
                    Dashboard.kimaSpherePosition = kSpherePosition;
                }

                Memory.WriteInt32((IntPtr)(Functions.Offsets(23)), sTotalAP);
                Memory.WriteInt32((IntPtr)(Functions.Offsets(24)), sCurrentAP);

                for (int i = 0; i < 4; i++)
                {
                    int kHPMPHolder = Memory.ReadInt32((IntPtr)(Functions.Offsets(25) + (0x04 * i)));
                    Memory.WriteInt32((IntPtr)(Functions.Offsets(7) + (0x04 * i)), kHPMPHolder);
                }

                for (int i = 0; i < 8; i++)
                {
                    byte kStatHolder = Memory.ReadByte((IntPtr)(Functions.Offsets(29) + i));
                    Memory.WriteByte((IntPtr)(Functions.Offsets(11) + i), kStatHolder);
                }

                int kSkillSetHolder1 = Memory.ReadInt32((IntPtr)(Functions.Offsets(30)));
                kSkillSetHolder1 = (kSkillSetHolder1 + 0x40000);
                Memory.WriteInt32((IntPtr)(Functions.Offsets(12)), kSkillSetHolder1);

                int kSkillSetHolder2 = Memory.ReadInt32((IntPtr)(Functions.Offsets(31)));
                Memory.WriteInt32((IntPtr)(Functions.Offsets(13)), kSkillSetHolder2);

                int kSkillSetHolder3 = Memory.ReadInt32((IntPtr)(Functions.Offsets(32)));
                Memory.WriteInt32((IntPtr)(Functions.Offsets(14)), kSkillSetHolder3);

                int kSkillSetHolder4 = Memory.ReadInt32((IntPtr)(Functions.Offsets(33)));
                kSkillSetHolder4 = (kSkillSetHolder4 + 0x17F10000);
                Memory.WriteInt32((IntPtr)(Functions.Offsets(15)), kSkillSetHolder4);

                int sOverdrivesAvailableHolder = Memory.ReadInt32((IntPtr)(Functions.Offsets(16)));
                Memory.WriteInt32((IntPtr)(Functions.Offsets(34)), sOverdrivesAvailableHolder);

                byte[] kOverdriveActiveHolder = { 0x00 };
                kOverdriveActiveHolder[0] = Memory.ReadByte((IntPtr)(Functions.Offsets(35)));
                Memory.WriteByteArray((IntPtr)(Functions.Offsets(17)), kOverdriveActiveHolder);

                byte[] sOverdriveCharge = { 0x00 };
                sOverdriveCharge[0] = Memory.ReadByte((IntPtr)((Functions.Offsets(17) + 0x01)));
                Memory.WriteByteArray((IntPtr)((Functions.Offsets(35) + 0x01)), sOverdriveCharge);

                int ronsoRageTextPointer = Memory.ReadInt32((IntPtr)(Functions.Offsets(49)));
                ronsoRageTextPointer = (ronsoRageTextPointer + 0xA963);
                Memory.WriteByteArray((IntPtr)(ronsoRageTextPointer), ronsoRageNewName);

                #endregion

                #region "Game Has Changed Party Formation"

                if ((storyProgress == 492) && (Dashboard.justOnce[3] == false))
                {
                    Memory.WriteByteArray((IntPtr)((Functions.Offsets(44))), writePartyFormation);
                    Dashboard.justOnce[3] = true;
                }

                #endregion

                #region "Party Formation Variable Intitialization"

                readPartyFormation = Memory.ReadByteArray((IntPtr)((Functions.Offsets(44))), 8);
                int whereYAt = 0;
                int whereKAt = 0;

                for (int i = 0; i < 7; i++)
                {
                    if (readPartyFormation[i] == 0x01)
                    {
                        whereYAt = i;
                    }
                    else if (readPartyFormation[i] == 0x03)
                    {
                        whereKAt = i;
                    }
                }

                #endregion

                #region "Overdrive"

                if (Dashboard.justOnce[0] == false)
                {
                    Dashboard.ronsoRageMenuAssemblyBytesControl2 = Memory.ReadByteArray((IntPtr)Functions.Offsets(52), 5);
                    Dashboard.ronsoRageMenuAssemblyBytesControl3 = Memory.ReadByteArray((IntPtr)Functions.Offsets(57), 5);

                    int ronsoRageMenuAssemblyBytesAddress = Functions.Offsets(59);
                    int ronsoRageMenuAssemblyBytesAddress2 = Functions.Offsets(59);
                    ronsoRageMenuAssemblyBytesAddress2 = ronsoRageMenuAssemblyBytesAddress2 + 100;
                    byte[] ronsoRageMenuAssemblyBytesHolder = BitConverter.GetBytes(ronsoRageMenuAssemblyBytesAddress);
                    byte[] ronsoRageMenuAssemblyBytesHolder2 = BitConverter.GetBytes(ronsoRageMenuAssemblyBytesAddress2);

                    for (int i = 0; i < 4; i++)
                    {
                        Dashboard.ronsoRageMenuAssemblyBytes[(i + 1)] = ronsoRageMenuAssemblyBytesHolder[i];
                        Dashboard.ronsoRageMenuAssemblyBytes2[(i + 1)] = ronsoRageMenuAssemblyBytesHolder2[i];
                    }
                    Dashboard.ronsoRageMenuAssemblyBytes[0] = 0xA3;
                    Dashboard.ronsoRageMenuAssemblyBytes2[0] = 0xBE;
                    Dashboard.justOnce[0] = true;
                }

                int numberOfOverdrivesToShow = 1;
                byte numberOfOverdriveTypesToShow = Memory.ReadByte((IntPtr)Functions.Offsets(61));

                if (sTotalFights < 5)
                {
                    for (int i = 2; i < 17; i = i + 2)
                    {
                        sOverdriveOptions[i] = 0xFF;
                        sOverdriveOptions[(i + 1)] = 0x00;
                        Dashboard.skipThisLoop = false;

                        kOverdriveMenuEdit[(i * 2)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 1)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 2)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 3)] = 0x00;
                    }
                }
                else if (sTotalFights < 10)
                {
                    for (int i = 4; i < 18; i = i + 2)
                    {
                        sOverdriveOptions[i] = 0xFF;
                        sOverdriveOptions[(i + 1)] = 0x00;
                        Dashboard.skipThisLoop = false;

                        kOverdriveMenuEdit[(i * 2)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 1)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 2)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 3)] = 0x00;
                    }
                    numberOfOverdrivesToShow = 2;
                }
                else if (sTotalFights < 15)
                {
                    for (int i = 6; i < 18; i = i + 2)
                    {
                        sOverdriveOptions[i] = 0xFF;
                        sOverdriveOptions[(i + 1)] = 0x00;
                        Dashboard.skipThisLoop = false;

                        kOverdriveMenuEdit[(i * 2)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 1)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 2)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 3)] = 0x00;
                    }
                    numberOfOverdrivesToShow = 3;
                }
                else if (sTotalFights < 85)
                {
                    for (int i = 8; i < 18; i = i + 2)
                    {
                        sOverdriveOptions[i] = 0xFF;
                        sOverdriveOptions[(i + 1)] = 0x00;
                        Dashboard.skipThisLoop = false;

                        kOverdriveMenuEdit[(i * 2)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 1)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 2)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 3)] = 0x00;
                    }
                    numberOfOverdrivesToShow = 4;
                }
                else if (sTotalFights < 90)
                {
                    for (int i = 10; i < 18; i = i + 2)
                    {
                        sOverdriveOptions[i] = 0xFF;
                        sOverdriveOptions[(i + 1)] = 0x00;
                        Dashboard.skipThisLoop = false;

                        kOverdriveMenuEdit[(i * 2)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 1)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 2)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 3)] = 0x00;
                    }
                    numberOfOverdrivesToShow = 5;
                }
                else if (sTotalFights < 95)
                {
                    for (int i = 12; i < 18; i = i + 2)
                    {
                        sOverdriveOptions[i] = 0xFF;
                        sOverdriveOptions[(i + 1)] = 0x00;
                        Dashboard.skipThisLoop = false;

                        kOverdriveMenuEdit[(i * 2)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 1)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 2)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 3)] = 0x00;
                    }
                    numberOfOverdrivesToShow = 6;
                }
                else if (sTotalFights < 100)
                {
                    for (int i = 14; i < 18; i = i + 2)
                    {
                        sOverdriveOptions[i] = 0xFF;
                        sOverdriveOptions[(i + 1)] = 0x00;
                        Dashboard.skipThisLoop = false;

                        kOverdriveMenuEdit[(i * 2)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 1)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 2)] = 0x00;
                        kOverdriveMenuEdit[((i * 2) + 3)] = 0x00;
                    }
                    numberOfOverdrivesToShow = 7;
                }
                else
                {
                    numberOfOverdrivesToShow = 8;
                }

                if ((animaUnlocked == 0x11) && (Dashboard.skipThisLoop == false))
                {
                    for (int i = 0; i < 18; i++)
                    {
                        if (sOverdriveOptions[i] == 0xFF)
                        {
                            sOverdriveOptions[i] = 0x67;
                            sOverdriveOptions[i + 1] = 0x40;
                            Dashboard.skipThisLoop = true;
                            break;
                        }
                    }

                    for (int i = 0; i < 40; i++)
                    {
                        if (kOverdriveMenuEdit[i] == 0x00)
                        {
                            kOverdriveMenuEdit[i] = 0x67;
                            kOverdriveMenuEdit[i + 1] = 0x40;
                            kOverdriveMenuEdit[i + 2] = 0x01;
                            kOverdriveMenuEdit[i + 3] = 0x01;
                            break;
                        }
                    }
                    numberOfOverdrivesToShow++;
                }

                byte overdriveMenuIsOpen = Memory.ReadByte((IntPtr)(Functions.Offsets(4)));
                byte hoveringOverOverdrives = Memory.ReadByte((IntPtr)(Functions.Offsets(53)));
                byte charMakingMenuSelection = Memory.ReadByte((IntPtr)(Functions.Offsets(55)));

                if ((overdriveMenuIsOpen == 0x01) && (charMakingMenuSelection == whereKAt) && (hoveringOverOverdrives == 0x00))
                {
                    if (Dashboard.nopeCodeInjected == false)
                    {
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(22)), nopeCode);
                        Dashboard.nopeCodeInjected = true;
                    }

                    if (Dashboard.noperCodeInjected == false)
                    {
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(60)), noperCode);
                        Dashboard.noperCodeInjected = true;
                    }

                    if (Dashboard.augmentInjected == false)
                    {
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(52)), Dashboard.ronsoRageMenuAssemblyBytes);
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(57)), Dashboard.ronsoRageMenuAssemblyBytes2);
                        Dashboard.augmentInjected = true;

                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(51)), kOverdriveMenuEdit);
                    }

                    Memory.WriteByte((IntPtr)(Functions.Offsets(56)), (byte)numberOfOverdrivesToShow);
                }
                else if (overdriveMenuIsOpen == 0x01)
                {
                    if (Dashboard.augmentInjected == true)
                    {
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(52)), Dashboard.ronsoRageMenuAssemblyBytesControl2);
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(57)), Dashboard.ronsoRageMenuAssemblyBytesControl3);
                        Dashboard.augmentInjected = false;
                    }

                    if ((charMakingMenuSelection != whereKAt))
                    {
                        if (Dashboard.nopeCodeInjected == true)
                        {
                            Memory.WriteByteArray((IntPtr)(Functions.Offsets(22)), ronsoRageMenuAssemblyBytesControl1);
                            Dashboard.nopeCodeInjected = false;
                        }

                        if (Dashboard.noperCodeInjected == true)
                        {
                            Memory.WriteByteArray((IntPtr)(Functions.Offsets(60)), ronsoRageMenuAssemblyBytesControl4);
                            Dashboard.noperCodeInjected = false;
                        }
                    }
                    else if ((charMakingMenuSelection == whereKAt) && (hoveringOverOverdrives == 0x01))
                    {
                        Memory.WriteByte((IntPtr)(Functions.Offsets(56)), numberOfOverdriveTypesToShow);
                    }
                }
                else if (overdriveMenuIsOpen == 0x00)
                {
                    if (Dashboard.augmentInjected == true)
                    {
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(52)), Dashboard.ronsoRageMenuAssemblyBytesControl2);
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(57)), Dashboard.ronsoRageMenuAssemblyBytesControl3);
                        Dashboard.augmentInjected = false;
                    }

                    if (Dashboard.nopeCodeInjected == true)
                    {
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(22)), ronsoRageMenuAssemblyBytesControl1);
                        Dashboard.nopeCodeInjected = false;
                    }

                    if (Dashboard.noperCodeInjected == true)
                    {
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(60)), ronsoRageMenuAssemblyBytesControl4);
                        Dashboard.noperCodeInjected = false;
                    }
                }

                bool forceOutOfViewBool = false;
                byte[] forceOutOfView = new byte[2];
                forceOutOfView = Memory.ReadByteArray((IntPtr)(Functions.Offsets(51)), 2);

                if (Enumerable.SequenceEqual(forceOutOfView, forceOutOfViewControl))
                {
                    forceOutOfViewBool = true;
                }

                forceOutOfView = Memory.ReadByteArray((IntPtr)(Functions.Offsets(59)), 2);

                if (Enumerable.SequenceEqual(forceOutOfView, forceOutOfViewControl))
                {
                    forceOutOfViewBool = true;
                }

                if ((charMakingMenuSelection != whereKAt) && (forceOutOfViewBool))
                {
                    Memory.WriteByte((IntPtr)(Functions.Offsets(58)), 0x00);
                    Memory.WriteByte((IntPtr)(Functions.Offsets(58) + 0x7B), 0x0F);
                    Memory.WriteByte((IntPtr)(Functions.Offsets(58) + 0x98), 0xFF);
                }

                #endregion

                #region "Add Anima in Combat"

                if ((animaUnlocked != 0x11) && (combatIdentifier > 0))
                {
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(45)), kInPartyArray);
                    Dashboard.fakeAnimaUnlock = true;
                }
                else if ((Dashboard.fakeAnimaUnlock) && (combatIdentifier == 0))
                {
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(45)), aeonArray);
                    Dashboard.fakeAnimaUnlock = false;
                }

                #endregion

                #region "Party Formation, Overdrive, and Aeon Edits"

                int gridOpen = Memory.ReadByte((IntPtr)(Functions.Offsets(63)));
                int equipMenuOpen = Memory.ReadByte((IntPtr)(Functions.Offsets(71)));
                int statusMenuOpen = Memory.ReadByte((IntPtr)(Functions.Offsets(46)));

                if ((combatIdentifier == 0) && (Dashboard.weHaveBeenInCombat == false))
                {
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(18)), kInPartyArray);
                    for (int i = 0; i < 8; i++)
                    {
                        if (readPartyFormation[i] == 7)
                        {
                            readPartyFormation[i] = 3;
                            Memory.WriteByteArray((IntPtr)((Functions.Offsets(44))), readPartyFormation);
                        }
                    }

                    if (Dashboard.nopCodeInjected == true)
                    {
                        Memory.WriteByteArray((IntPtr)((Functions.Offsets(50))), combatAssemblyBytes);
                        Dashboard.nopCodeInjected = false;
                    }

                    Dashboard.painWasRemoved = false;
                    Dashboard.removeAnimaNow = false;
                    Dashboard.aeonListAffected = false;
                }
                else if ((combatIdentifier == 0) && (Dashboard.weHaveBeenInCombat == true))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        int sCurrentHPMPHolder = Memory.ReadInt32((IntPtr)(Functions.Offsets(7) + (0x04 * i)));
                        Memory.WriteInt32((IntPtr)(Functions.Offsets(25) + (0x04 * i)), sCurrentHPMPHolder);
                    }
                    Dashboard.weHaveBeenInCombat = false;
                }
                else if (combatIdentifier > 0)
                {
                    Memory.WriteByte((IntPtr)(Functions.Offsets(18)), 0x10);
                    Dashboard.weHaveBeenInCombat = true;
                    for (int i = 0; i < 8; i++)
                    {
                        if (readPartyFormation[i] == 3)
                        {
                            readPartyFormation[i] = 7;
                            Memory.WriteByteArray((IntPtr)((Functions.Offsets(44))), readPartyFormation);
                        }
                    }

                    int itsYourTurn = Memory.ReadByte((IntPtr)(Functions.Offsets(19)));
                    bool yAnimaIndex = false;
                    bool wasSeymoursTurn = false;
                    bool wasYunasTurn = false;
                    bool sValeforIndex = false;

                    #region "Remove Anima Hotfix"

                    sinHotfix[0] = Memory.ReadInt32((IntPtr)(combatIdentifier + 0x5D0));
                    sinHotfix[1] = Memory.ReadInt32((IntPtr)(combatIdentifier + 0x5D4));
                    sinHotfix[2] = Memory.ReadInt32((IntPtr)(combatIdentifier + 0x594));
                    sinHotfix[3] = Memory.ReadInt32((IntPtr)(combatIdentifier + 0x598));

                    int n = 0;
                    for (int i = 4; i < 12; i++)
                    {
                        sinHotfix[i] = Memory.ReadByte((IntPtr)((combatIdentifier + 0x5A8) + n));
                        n++;
                    }

                    if ((Enumerable.SequenceEqual(sinGenHotfixConstant, sinHotfix)) || (Enumerable.SequenceEqual(sinGenHotfixConstant2, sinHotfix)) || (Enumerable.SequenceEqual(sinGenHotfixConstant3, sinHotfix)))
                    {
                        Dashboard.removeAnimaNow = true;
                    }

                    #endregion

                    if (((itsYourTurn == 0x01) || (itsYourTurn == 0x07)) && (Dashboard.aeonListAffected == false))
                    {
                        Dashboard.yAeonsAvailable = Memory.ReadByteArray((IntPtr)(Functions.Offsets(47)), 20);
                        Dashboard.sAeonsAvailable = Memory.ReadByteArray((IntPtr)(Functions.Offsets(47)), 20);
                    }

                    if (Dashboard.yAeonsAvailable[0] != 0)
                    {
                        if (Dashboard.nopCodeInjected == false)
                        {
                            Memory.WriteByteArray((IntPtr)((Functions.Offsets(50))), nopCode);
                            Dashboard.nopCodeInjected = true;
                        }

                        int yNumOfAeons = 0;
                        foreach (int number in Dashboard.yAeonsAvailable)
                        {
                            if (number == 0x0D)
                            {
                                yAnimaIndex = true;
                            }
                            if ((number != 0x00) && (number != 0xFF) && (number != 0x30))
                            {
                                yNumOfAeons++;
                            }
                        }
                        foreach (int number in Dashboard.sAeonsAvailable)
                        {
                            if (number == 0x08)
                            {
                                sValeforIndex = true;
                            }
                        }

                        if ((itsYourTurn == 0x01) && (animaUnlocked != 0x11))
                        {
                            if (yAnimaIndex)
                            {
                                for (int i = yNumOfAeons; i < 16; i++)
                                {
                                    Dashboard.yAeonsAvailable[i] = Dashboard.yAeonsAvailable[(i + 2)];
                                }
                                Memory.WriteByteArray((IntPtr)((Functions.Offsets(47))), Dashboard.yAeonsAvailable);
                                Dashboard.aeonListAffected = true;
                                wasYunasTurn = false;
                            }
                            else if (wasSeymoursTurn == false)
                            {
                                Memory.WriteByteArray((IntPtr)((Functions.Offsets(47))), Dashboard.yAeonsAvailable);
                                Dashboard.aeonListAffected = true;
                                wasSeymoursTurn = true;
                            }
                        }
                        else if ((itsYourTurn == 0x07) && (animaUnlocked != 0x11))
                        {
                            if (sValeforIndex)
                            {
                                for (int i = 0; i < 16; i++)
                                {
                                    Dashboard.sAeonsAvailable[i] = Dashboard.sAeonsAvailable[(i + 2)];
                                }
                                Dashboard.aeonListAffected = true;
                                wasSeymoursTurn = false;
                                Memory.WriteByteArray((IntPtr)((Functions.Offsets(47))), Dashboard.sAeonsAvailable);

                            }
                            else if (wasYunasTurn == false)
                            {
                                Dashboard.aeonListAffected = true;
                                wasYunasTurn = true;
                                Memory.WriteByteArray((IntPtr)((Functions.Offsets(47))), Dashboard.sAeonsAvailable);
                            }
                        }
                    }

                    int switchToSummonPointer = Memory.ReadInt32((IntPtr)Functions.Offsets(43));

                    int sOverdriveOptionsPointer = 0;
                    if (itsYourTurn == 0x07)
                    {
                        if (Dashboard.removeAnimaNow == true)
                        {
                            switchToSummon[2] = 0xFF;
                            switchToSummon[3] = 0x00;
                            Memory.WriteByteArray((IntPtr)(switchToSummonPointer), switchToSummon);
                        }
                        else
                        {
                            Memory.WriteByteArray((IntPtr)(switchToSummonPointer), switchToSummon);
                        }

                        Memory.WriteByteArray((IntPtr)((switchToSummonPointer + 0x28)), sOverdriveGeneralEdit);
                        sOverdriveOptionsPointer = ((Memory.ReadInt32((IntPtr)(Functions.Offsets(43)))) + 0x128);
                        Memory.WriteByteArray((IntPtr)sOverdriveOptionsPointer, sOverdriveOptions);
                    }

                    byte[] removePain = new byte[20];
                    if ((itsYourTurn == 0x0D) && (animaUnlocked != 0x11) && (Dashboard.painWasRemoved == false))
                    {
                        removePain = Memory.ReadByteArray((IntPtr)(switchToSummonPointer), 20);
                        for (int i = 2; i < 16; i = i + 2)
                        {
                            removePain[i] = removePain[(i + 2)];
                            removePain[(i + 1)] = removePain[(i + 3)];
                        }
                        Memory.WriteByteArray((IntPtr)(switchToSummonPointer), removePain);
                        Dashboard.painWasRemoved = true;
                    }
                    if ((itsYourTurn == 0x00) || (itsYourTurn == 0x01) || (itsYourTurn == 0x02) || (itsYourTurn == 0x03) || (itsYourTurn == 0x04) || (itsYourTurn == 0x05) || (itsYourTurn == 0x06) || (itsYourTurn == 0x07))
                    {
                        Dashboard.painWasRemoved = false;
                    }
                }

                #endregion

                #region "Celestial Weapon"

                int movieSphereBought = Memory.ReadByte((IntPtr)(Functions.Offsets(79)));
                int keyItems1 = Memory.ReadByte((IntPtr)(Functions.Offsets(80)));
                int keyItems2 = Memory.ReadByte((IntPtr)(Functions.Offsets(81)));
                bool celestialWepUnlocked = false;
                if (animaUnlocked == 0x11)
                {
                    celestialWeapon[0] = true;
                }
                if (movieSphereBought >= 0x13)
                {
                    celestialWeapon[1] = true;
                }
                if (((keyItems1 / 8) % 2) != 0)
                {
                    celestialWeapon[2] = true;
                }

                for (int i = 4; i < 255; i = i + 8)
                {
                    if ((keyItems2 == i) || (keyItems2 == (i + 1)) || (keyItems2 == (i + 2)) || (keyItems2 == (i + 3)))
                    {
                        celestialWeapon[3] = true;
                    }
                }
                for (int i = 1; i < 4; i = i + 2)
                {
                    for (int n = 0; n < 64; n++)
                    {
                        if (keyItems2 == ((i * 64) + n))
                        {
                            celestialWeapon[4] = true;
                        }
                    }
                }

                if ((celestialWeapon[0] == true) && (celestialWeapon[1] == true) && (celestialWeapon[2] == true) && (celestialWeapon[3] == true) && (celestialWeapon[4] == true))
                {
                    celestialWepUnlocked = true;
                    if ((Dashboard.celeWepOnce == false) || ((Dashboard.shopReset == true) && (shopIsOpen == 0) && (customizeMenuOpen == 0)))
                    {
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (23 * 22)), sCelestialWeaponShatter1);
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (23 * 22) + 7), sCelestialWeaponShatter2);
                        Memory.WriteByte((IntPtr)(Functions.Offsets(69) + (23 * 22) + 4), 0x03);
                        if ((Dashboard.justOnce[1] == false) || (customizeMenuOpen == 0x01))
                        {
                            Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (23 * 22) + 6), ffArray);
                        }
                        Dashboard.celeWepOnce = true;
                    }

                    int itsYourTurn = Memory.ReadByte((IntPtr)(Functions.Offsets(19)));
                    int staffRenamePointer = Memory.ReadInt32((IntPtr)(Functions.Offsets(91)));
                    staffRenamePointer = (staffRenamePointer + 0x304A);
                    if (((combatIdentifier > 0) && (itsYourTurn == 0x07)) || ((charMakingMenuSelection == whereKAt) && (equipMenuOpen == 1)))
                    {
                        Memory.WriteByteArray((IntPtr)(staffRenamePointer), staffToSName);
                    }
                    else
                    {
                        Memory.WriteByteArray((IntPtr)(staffRenamePointer), staffNameReset);
                    }
                }

                #endregion

                #region "Equipment"

                int appendGear = 0;

                bool isYWearingWep = false;
                bool isYWearingArm = false;
                bool isKWearingWep = false;
                bool isKWearingArm = false;

                if ((itemsMenuOpen == 1) && (menuIsOpen == 1) && (customizeMenuOpen != 1) && (overdriveMenuIsOpen != 1))
                {
                    if (hoveringOverOverdrives == 1)
                    {
                        Memory.WriteByte((IntPtr)(Functions.Offsets(53)), 0x00);
                    }
                    if (undoItemsAction == 1)
                    {
                        Memory.WriteByte((IntPtr)(Functions.Offsets(70)), 0x00);
                    }
                }

                if ((shopIsOpen == 0) && (combatIdentifier == 0) && (itemsMenuOpen != 1) && (customizeMenuOpen != 1))
                {
                    Dashboard.combatGearReset[0] = false;
                    Dashboard.combatGearReset[1] = false;

                    if (Dashboard.shopReset == true)
                    {
                        for (int i = 0; i < 115; i++)
                        {
                            gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                            if ((gearArray[4] == 0x01) && (gearArray[2] == 0) && ((gearArray[0] == Dashboard.whatDidYouHave[2]) || (gearArray[0] == Dashboard.whatDidYouHave[3])))
                            {
                                if (gearArray[0] <= 0x49)
                                {
                                    Dashboard.whatDidYouHave[2] = 0;
                                }
                                else if (gearArray[0] > 0x49)
                                {
                                    Dashboard.whatDidYouHave[3] = 0;
                                }
                            }
                        }

                        bool skipThat = false;
                        for (int i = 0; i < 115; i++)
                        {
                            gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                            if ((gearArray[4] == 0x03) && (gearArray[6] == 0x03) && (gearArray[0] <= 0x49))
                            {
                                skipThat = true;
                                break;
                            }
                        }

                        if (skipThat == false)
                        {
                            for (int i = 0; i < 115; i++)
                            {
                                gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                                if ((gearArray[4] == 0x01) && (gearArray[0] != 0x00) && (gearArray[6] != 0x01) && (gearArray[0] <= 0x49) && (gearArray[2] != 0))
                                {
                                    gearArray[4] = 0x03;
                                    gearArray[6] = 0x03;

                                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (appendGear * 22)), gearArray);
                                    isKWearingWep = true;

                                    appendGear++;
                                    break;
                                }
                            }
                            if ((isKWearingWep == false) && (celestialWepUnlocked == false))
                            {
                                Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (26 * 22)), yHasNoWeapon);
                            }
                            else if ((isKWearingWep == false) && (celestialWepUnlocked == true))
                            {
                                Memory.WriteByte((IntPtr)(Functions.Offsets(69) + (23 * 22) + 4), 0x03);
                                Memory.WriteByte((IntPtr)(Functions.Offsets(69) + (23 * 22) + 6), 0x03);
                            }
                        }

                        for (int i = 0; i < 115; i++)
                        {
                            gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                            if ((gearArray[4] == 0x03) && ((gearArray[0] == Dashboard.whatDidYouHave[2]) || (gearArray[0] == Dashboard.whatDidYouHave[3])))
                            {
                                gearArray[6] = 0x03;
                                Memory.WriteByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), gearArray);
                            }
                        }

                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (24 * 22)), emptyGearArray);
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (25 * 22)), emptyGearArray);

                        Dashboard.shopReset = false;
                        Dashboard.shopJustOnce = false;
                    }

                    if ((Dashboard.equipAugmentInjected == false) && (charMakingMenuSelection == whereKAt) && ((equipMenuOpen == 1) || (statusMenuOpen == 1)))
                    {
                        iconAssemblyCode[2] = 0x02;
                        equipNameAssemblyCode[3] = 0x02;
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(72)), iconAssemblyCode);
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(73)), equipNameAssemblyCode);
                        Dashboard.equipAugmentInjected = true;
                    }
                    else if ((Dashboard.equipAugmentInjected == true) && (!((charMakingMenuSelection == whereKAt) && ((equipMenuOpen == 1) || (statusMenuOpen == 1)))))
                    {
                        iconAssemblyCode[2] = 0x04;
                        equipNameAssemblyCode[3] = 0x04;
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(72)), iconAssemblyCode);
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(73)), equipNameAssemblyCode);
                        Dashboard.equipAugmentInjected = false;
                    }

                    if (Dashboard.kGearRemoved == false)
                    {
                        for (int i = 0; i < 115; i++)
                        {
                            gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                            if (gearArray[4] == 0x03)
                            {
                                gearArray[2] = 0x00;
                                gearArray[4] = 0x08;
                                Memory.WriteByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), gearArray);
                            }
                            else if (gearArray[4] == 0x07)
                            {
                                gearArray[2] = 0x00;
                                gearArray[4] = 0x08;
                                Memory.WriteByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), gearArray);
                            }
                        }
                        for (int i = 0; i < 25; i++)
                        {
                            Memory.WriteByte((IntPtr)(Functions.Offsets(69) + (i * 22) + 6), 0xFF);
                            i++;
                        }
                        Dashboard.kGearRemoved = true;
                        Dashboard.celeWepOnce = false;
                    }

                    appendGear = 0;

                    for (int i = 0; i < 115; i++)
                    {
                        gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                        if (gearArray[4] == 0x07)
                        {
                            gearArray[4] = 0x03;
                            if (gearArray[6] == 0x07)
                            {
                                gearArray[6] = 0x03;
                            }
                            Memory.WriteByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), gearArray);
                        }
                    }

                    for (int i = 0; i < 115; i++)
                    {
                        gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                        if ((gearArray[4] == 0x01) && (gearArray[0] != 0x00) && (gearArray[2] != 0))
                        {
                            gearArray[4] = 0x03;
                            gearArrayShatter1 = gearArray.Take(6).ToArray();

                            int l = 0;
                            for (int n = 7; n < 22; n++)
                            {
                                gearArrayShatter2[l] = gearArray[n];
                                l++;
                            }
                            Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (appendGear * 22)), gearArrayShatter1);
                            Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (appendGear * 22) + 7), gearArrayShatter2);
                            appendGear++;
                        }
                    }

                    appendGear = 0;
                    if (Dashboard.justOnce[1] == false)
                    {
                        for (int i = 0; i < 115; i++)
                        {
                            gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                            if ((gearArray[4] == 0x01) && (gearArray[0] != 0x00) && (gearArray[6] != 0x01) && (gearArray[0] <= 0x49) && (gearArray[2] != 0))
                            {
                                gearArray[4] = 0x03;
                                gearArray[6] = 0x03;

                                Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (appendGear * 22)), gearArray);
                                isKWearingWep = true;

                                appendGear++;
                                break;
                            }
                        }
                        if ((isKWearingWep == false) && (celestialWepUnlocked == true))
                        {
                            Memory.WriteByte((IntPtr)(Functions.Offsets(69) + (23 * 22) + 4), 0x03);
                            Memory.WriteByte((IntPtr)(Functions.Offsets(69) + (23 * 22) + 6), 0x03);
                        }
                        else if (isKWearingWep == false)
                        {
                            Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (26 * 22)), yHasNoWeapon);
                        }
                        Dashboard.justOnce[1] = true;
                    }

                    for (int i = 0; i < 115; i++)
                    {
                        gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                        if ((gearArray[4] == 0x01) && (gearArray[6] == 0x01))
                        {
                            if (gearArray[0] <= 0x49)
                            {
                                yChangeEquipped[0] = (byte)i;
                                Dashboard.whatDidYouHave[0] = gearArray[0];
                            }
                            else if (gearArray[0] > 0x49)
                            {
                                yChangeEquipped[1] = (byte)i;
                                Dashboard.whatDidYouHave[1] = gearArray[0];
                            }
                        }

                        if ((gearArray[4] == 0x03) && (gearArray[6] == 0x03))
                        {
                            if (gearArray[0] <= 0x49)
                            {
                                kChangeEquipped[0] = (byte)i;
                                Dashboard.whatDidYouHave[2] = gearArray[0];
                            }
                            else if (gearArray[0] > 0x49)
                            {
                                kChangeEquipped[1] = (byte)i;
                                Dashboard.whatDidYouHave[3] = gearArray[0];
                            }
                        }
                    }

                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(74)), kChangeEquipped);
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(75)), kChangeEquipped);
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(78)), yChangeEquipped);

                    if ((equipMenuOpen == 0x01) && (charMakingMenuSelection == whereYAt))
                    {
                        for (int i = 0; i < 35; i++)
                        {
                            gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(69) + (i * 22)), 22);
                            if ((gearArray[6] == 0x03) && ((Dashboard.whatDidYouHave[0] == gearArray[0]) || (Dashboard.whatDidYouHave[1] == gearArray[0])))
                            {
                                gearArray[6] = 0xFF;
                                Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (i * 22)), gearArray);
                                if (gearArray[0] <= 0x49)
                                {
                                    isKWearingWep = false;
                                }
                                else if (gearArray[0] > 0x49)
                                {
                                    isKWearingArm = false;
                                }
                            }
                        }
                    }
                    else if ((equipMenuOpen == 0x01) && (charMakingMenuSelection == whereKAt))
                    {
                        for (int i = 0; i < 90; i++)
                        {
                            gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                            if ((gearArray[6] == 0x01) && ((Dashboard.whatDidYouHave[2] == gearArray[0]) || (Dashboard.whatDidYouHave[3] == gearArray[0])))
                            {
                                gearArray[6] = 0xFF;
                                Memory.WriteByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), gearArray);
                                if (gearArray[0] <= 0x49)
                                {
                                    isYWearingWep = false;
                                }
                                else if (gearArray[0] > 0x49)
                                {
                                    isYWearingArm = false;
                                }
                            }
                        }
                    }
                    for (int i = 0; i < 115; i++)
                    {
                        gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                        if ((gearArray[2] == 0x01) && (gearArray[4] == 0x01) && (gearArray[6] == 0x01))
                        {
                            if (gearArray[0] <= 0x49)
                            {
                                isYWearingWep = true;
                            }
                            else if (gearArray[0] > 0x49)
                            {
                                isYWearingArm = true;
                            }
                        }
                        else if ((gearArray[2] == 0x01) && (gearArray[4] == 0x03) && (gearArray[6] == 0x03))
                        {
                            if (gearArray[0] <= 0x49)
                            {
                                isKWearingWep = true;
                            }
                            else if (gearArray[0] > 0x49)
                            {
                                isKWearingArm = true;
                            }
                        }
                    }
                    if (isYWearingWep == false)
                    {
                        for (int i = 0; i < 90; i++)
                        {
                            gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                            if ((gearArray[4] == 0x01) && (gearArray[0] <= 0x49))
                            {
                                if (charMakingMenuSelection == whereYAt)
                                {
                                    gearArray[6] = 0x01;
                                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), gearArray);
                                    isYWearingWep = true;
                                    break;
                                }
                                else
                                {
                                    if (gearArray[0] != Dashboard.whatDidYouHave[2])
                                    {
                                        gearArray[6] = 0x01;
                                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), gearArray);
                                        isYWearingWep = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (isYWearingArm == false)
                    {
                        yChangeEquipped[1] = 0xFF;
                        Dashboard.whatDidYouHave[1] = 0xFF;
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(78)), yChangeEquipped);
                    }
                    if (isKWearingWep == false)
                    {
                        for (int i = 0; i < 35; i++)
                        {
                            gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(69) + (i * 22)), 22);
                            if ((gearArray[4] == 0x03) && (gearArray[0] <= 0x49))
                            {
                                if (charMakingMenuSelection == whereKAt)
                                {
                                    gearArray[6] = 0x03;
                                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (i * 22)), gearArray);
                                    isKWearingWep = true;
                                    break;
                                }
                                else
                                {
                                    if (gearArray[0] != Dashboard.whatDidYouHave[0])
                                    {
                                        gearArray[6] = 0x03;
                                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (i * 22)), gearArray);
                                        isKWearingWep = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (isKWearingArm == false)
                    {
                        kChangeEquipped[1] = 0xFF;
                        Dashboard.whatDidYouHave[3] = 0xFF;
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(74)), kChangeEquipped);
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(75)), kChangeEquipped);
                    }
                }
                if ((shopIsOpen == 1) || ((itemsMenuOpen == 1) && (menuIsOpen == 1)) || (customizeMenuOpen == 1))
                {
                    for (int i = 0; i < 115; i++)
                    {
                        gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                        if ((gearArray[4] == 0x03) && (Enumerable.SequenceEqual(gearArray, kStartingWep) == false) && (Enumerable.SequenceEqual(gearArray, kStartingArm) == false))
                        {
                            Memory.WriteByte((IntPtr)(Functions.Offsets(68) + (i * 22) + 2), 0x00);

                            if (Dashboard.shopJustOnce == false)
                            {
                                if ((gearArray[6] == 0x03) && (gearArray[0] <= 0x49))
                                {
                                    Dashboard.whatDidYouHave[2] = gearArray[0];
                                }
                                else if ((gearArray[6] == 0x03) && (gearArray[0] > 0x49))
                                {
                                    Dashboard.whatDidYouHave[3] = gearArray[0];
                                }
                                Dashboard.shopJustOnce = true;
                            }

                            Memory.WriteByte((IntPtr)(Functions.Offsets(68) + (i * 22) + 6), 0xFF);
                        }
                    }
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (24 * 22)), kStartingWep);
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (25 * 22)), kStartingArm);

                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(74)), kStartingSet);

                    Dashboard.shopReset = true;
                }

                if (combatIdentifier > 0)
                {
                    appendGear = 0;
                    for (int i = 0; i < 115; i++)
                    {
                        gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                        if (gearArray[4] == 0x03)
                        {
                            gearArray[4] = 0x07;

                            if (gearArray[6] == 0x03)
                            {
                                gearArray[6] = 0x07;
                            }
                            Memory.WriteByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), gearArray);
                        }
                    }

                    byte removeEquipOptionWep = 0x00;
                    byte removeEquipOptionArm = 0x00;
                    byte yToSConvertionWep = 0x00;
                    byte yToSConvertionArm = 0x00;
                    int itsYourTurn = Memory.ReadByte((IntPtr)(Functions.Offsets(19)));
                    int switchToSummonPointer = Memory.ReadInt32((IntPtr)Functions.Offsets(43));

                    if ((Dashboard.inCombatAugmentInjected == false) && (itsYourTurn == 0x07))
                    {
                        inCombatIconAssemblyBytesControl[3] = 0x02;
                        inCombatNameAssemblyBytesControl[3] = 0x02;
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(85)), inCombatIconAssemblyBytesControl);
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(87)), inCombatNameAssemblyBytesControl);
                        Dashboard.inCombatAugmentInjected = true;
                    }
                    else if ((Dashboard.inCombatAugmentInjected == true) && (itsYourTurn != 0x07))
                    {
                        inCombatIconAssemblyBytesControl[3] = 0x04;
                        inCombatNameAssemblyBytesControl[3] = 0x04;
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(85)), inCombatIconAssemblyBytesControl);
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(87)), inCombatNameAssemblyBytesControl);
                        Dashboard.inCombatAugmentInjected = false;
                    }

                    for (int i = 0; i < 115; i++)
                    {
                        gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                        if ((gearArray[4] == 0x01) && (gearArray[6] == 0x01))
                        {
                            if (gearArray[0] <= 0x49)
                            {
                                if (Dashboard.whatDidYouHave[0] != gearArray[0])
                                {
                                    Dashboard.swappedGear[0] = true;
                                }
                                Dashboard.whatDidYouHave[0] = gearArray[0];
                            }
                            else if (gearArray[0] > 0x49)
                            {
                                if (Dashboard.whatDidYouHave[1] != gearArray[0])
                                {
                                    Dashboard.swappedGear[1] = true;
                                }
                                Dashboard.whatDidYouHave[1] = gearArray[0];
                            }
                        }

                        if ((gearArray[4] == 0x07) && (gearArray[6] == 0x07))
                        {
                            if (gearArray[0] <= 0x49)
                            {
                                if (Dashboard.whatDidYouHave[2] != gearArray[0])
                                {
                                    Dashboard.swappedGear[2] = true;
                                }
                                Dashboard.whatDidYouHave[2] = gearArray[0];
                            }
                            else if (gearArray[0] > 0x49)
                            {
                                if (Dashboard.whatDidYouHave[3] != gearArray[0])
                                {
                                    Dashboard.swappedGear[3] = true;
                                }
                                Dashboard.whatDidYouHave[3] = gearArray[0];
                            }
                        }
                    }

                    if (itsYourTurn == 0x01)
                    {
                        if (Dashboard.combatGearReset[0] == false)
                        {
                            Dashboard.yCombatGearArrayWepConstant = Memory.ReadByteArray((IntPtr)(switchToSummonPointer + 0x158), 21);
                            Dashboard.yCombatGearArrayArmConstant = Memory.ReadByteArray((IntPtr)(switchToSummonPointer + 0x2E8), 21);
                            Dashboard.combatGearReset[0] = true;
                        }

                        if (Dashboard.swappedGear[2] == true)
                        {
                            Memory.WriteByteArray((IntPtr)(switchToSummonPointer + 0x158), Dashboard.yCombatGearArrayWepConstant);
                            Dashboard.swappedGear[2] = false;
                        }
                        else if (Dashboard.swappedGear[3] == true)
                        {
                            Memory.WriteByteArray((IntPtr)(switchToSummonPointer + 0x2E8), Dashboard.yCombatGearArrayArmConstant);
                            Dashboard.swappedGear[3] = false;
                        }

                        for (int i = 0; i < 115; i++)
                        {
                            gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                            if ((gearArray[4] == 0x07) && (gearArray[6] == 0x07))
                            {
                                if (gearArray[0] <= 0x49)
                                {
                                    removeEquipOptionWep = gearArray[0];
                                }
                                else if (gearArray[0] > 0x49)
                                {
                                    removeEquipOptionArm = gearArray[0];
                                }
                            }
                        }

                        for (int i = 0; i < 115; i++)
                        {
                            gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                            if ((gearArray[0] == removeEquipOptionWep) && (gearArray[2] == 0x01) && (gearArray[4] == 0x01))
                            {
                                yToSConvertionWep = (byte)i;
                            }
                            else if ((gearArray[0] == removeEquipOptionArm) && (gearArray[2] == 0x01) && (gearArray[4] == 0x01))
                            {
                                yToSConvertionArm = (byte)i;
                            }
                        }

                        combatGearArray = Memory.ReadByteArray((IntPtr)(switchToSummonPointer + 0x158), 30);
                        for (int i = 0; i < 20; i++)
                        {
                            if (combatGearArray[i] == yToSConvertionWep)
                            {
                                for (int n = i; n < 20; n = n + 2)
                                {
                                    combatGearArray[n] = combatGearArray[(n + 2)];
                                    combatGearArray[(n + 1)] = combatGearArray[(n + 3)];
                                }
                                Memory.WriteByteArray((IntPtr)(switchToSummonPointer + 0x158), combatGearArray);
                                break;
                            }
                        }

                        combatGearArray = Memory.ReadByteArray((IntPtr)(switchToSummonPointer + 0x2E8), 30);
                        for (int i = 0; i < 20; i++)
                        {
                            if (combatGearArray[i] == yToSConvertionArm)
                            {
                                for (int n = i; n < 20; n = n + 2)
                                {
                                    combatGearArray[n] = combatGearArray[(n + 2)];
                                    combatGearArray[(n + 1)] = combatGearArray[(n + 3)];
                                }
                                Memory.WriteByteArray((IntPtr)(switchToSummonPointer + 0x2E8), combatGearArray);
                                break;
                            }
                        }
                    }
                    else if (itsYourTurn == 0x07)
                    {
                        if (Dashboard.combatGearReset[1] == false)
                        {
                            Dashboard.kCombatGearArrayWepConstant = Memory.ReadByteArray((IntPtr)(switchToSummonPointer + 0x158), 21);
                            Dashboard.kCombatGearArrayArmConstant = Memory.ReadByteArray((IntPtr)(switchToSummonPointer + 0x2E8), 21);
                            Dashboard.combatGearReset[1] = true;
                        }

                        if (Dashboard.swappedGear[0] == true)
                        {
                            Memory.WriteByteArray((IntPtr)(switchToSummonPointer + 0x158), Dashboard.kCombatGearArrayWepConstant);
                            Dashboard.swappedGear[0] = false;
                        }
                        else if (Dashboard.swappedGear[1] == true)
                        {
                            Memory.WriteByteArray((IntPtr)(switchToSummonPointer + 0x2E8), Dashboard.kCombatGearArrayArmConstant);
                            Dashboard.swappedGear[1] = false;
                        }

                        for (int i = 0; i < 115; i++)
                        {
                            gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                            if ((gearArray[4] == 0x01) && (gearArray[6] == 0x01))
                            {
                                if (gearArray[0] <= 0x49)
                                {
                                    removeEquipOptionWep = gearArray[0];
                                }
                                else if (gearArray[0] > 0x49)
                                {
                                    removeEquipOptionArm = gearArray[0];
                                }
                            }
                        }

                        for (int i = 0; i < 115; i++)
                        {
                            gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                            if ((gearArray[0] == removeEquipOptionWep) && (gearArray[2] == 0x01) && (gearArray[4] == 0x07))
                            {
                                yToSConvertionWep = (byte)i;
                            }
                            else if ((gearArray[0] == removeEquipOptionArm) && (gearArray[2] == 0x01) && (gearArray[4] == 0x07))
                            {
                                yToSConvertionArm = (byte)i;
                            }
                        }

                        combatGearArray = Memory.ReadByteArray((IntPtr)(switchToSummonPointer + 0x158), 30);
                        for (int i = 0; i < 20; i++)
                        {
                            if (combatGearArray[i] == yToSConvertionWep)
                            {
                                for (int n = i; n < 20; n = n + 2)
                                {
                                    combatGearArray[n] = combatGearArray[(n + 2)];
                                    combatGearArray[(n + 1)] = combatGearArray[(n + 3)];
                                }
                                Memory.WriteByteArray((IntPtr)(switchToSummonPointer + 0x158), combatGearArray);
                                break;
                            }
                        }

                        combatGearArray = Memory.ReadByteArray((IntPtr)(switchToSummonPointer + 0x2E8), 30);
                        for (int i = 0; i < 20; i++)
                        {
                            if (combatGearArray[i] == yToSConvertionArm)
                            {
                                for (int n = i; n < 20; n = n + 2)
                                {
                                    combatGearArray[n] = combatGearArray[(n + 2)];
                                    combatGearArray[(n + 1)] = combatGearArray[(n + 3)];
                                }
                                Memory.WriteByteArray((IntPtr)(switchToSummonPointer + 0x2E8), combatGearArray);
                                break;
                            }
                        }
                    }
                }

                for (int i = 0; i < 71; i++)
                {
                    gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                    if ((gearArray[4] == 0x03) && (gearArray[2] == 0x01))
                    {
                        gearArray[4] = 0x08;
                        gearArray[2] = 0x00;
                        gearArray[6] = 0xFF;
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), gearArray);
                    }
                }

                #endregion

                #region "Photo Changes"

                if ((gridOpen == 0x01) && (charMakingMenuSelection == whereKAt) && (Dashboard.photoBomb == false))
                {
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(62)), gridPhotoAssemblyBytes);
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(65)), gridPhotoEffectAssemblyBytes);
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(67)), gridPhotoEffectAssemblyBytes);
                    Dashboard.photoBomb = true;
                }
                else if (((gridOpen != 0x01) || (charMakingMenuSelection != whereKAt)) && (Dashboard.photoBomb == true))
                {
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(62)), gridPhotoAssemblyBytesControl);
                    Dashboard.photoBomb = false;
                }

                int hoveringOverMenus = Memory.ReadByte((IntPtr)(Functions.Offsets(89)));
                if ((Dashboard.equipPhotoRemoved[0] == false) && (charMakingMenuSelection == whereKAt) && (menuIsOpen == 0x01) && (((equipMenuOpen == 1) || (hoveringOverMenus == 4)) || ((statusMenuOpen == 1) || (hoveringOverMenus == 5))))
                {
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(86)), nopitCode);
                    Dashboard.equipPhotoRemoved[0] = true;
                }
                else if ((Dashboard.equipPhotoRemoved[0] == true) && (!((charMakingMenuSelection == whereKAt) && (((equipMenuOpen == 1) || (hoveringOverMenus == 4)) || ((statusMenuOpen == 1) || (hoveringOverMenus == 5))))))
                {
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(86)), equipPictureRemovalConstant);
                    Dashboard.equipPhotoRemoved[0] = false;
                }

                if ((Dashboard.equipPhotoRemoved[1] == false) && (menuIsOpen == 1) && ((equipMenuOpen == 1) || (statusMenuOpen == 1)))
                {
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(90)), nopyCode);
                    Dashboard.equipPhotoRemoved[1] = true;
                }
                else if (Dashboard.equipPhotoRemoved[1] == true)
                {
                    Memory.WriteByte((IntPtr)(Functions.Offsets(66)), (byte)charMakingMenuSelection);
                    if ((equipMenuOpen != 1) && (statusMenuOpen != 1))
                    {
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(90)), equipPictureEffectRemovalConstant);
                        Dashboard.equipPhotoRemoved[1] = false;
                    }
                }

                #endregion
            }
        }

        public void SeymourModOff()
        {
            if (Dashboard.seymourModHasBeenOn == true)
            {
                #region "Array Declaration"

                byte[] kNameReset =
                {
                    0x5A, 0x78, 0x7C, 0x70, 0x77, 0x81, 0x78
                };
                byte[] ronsoRageReset =
                {
                    0x61, 0x7E, 0x7D, 0x82, 0x7E, 0x3A, 0x61, 0x70, 0x76, 0x74,
                    0x00, 0x47, 0x00, 0x64, 0x82, 0x74, 0x3A, 0x75, 0x78, 0x74,
                    0x7D, 0x73, 0x82, 0x41, 0x3A, 0x82, 0x7A, 0x78, 0x7B, 0x7B,
                    0x82, 0x3A, 0x70, 0x76, 0x70, 0x78, 0x7D, 0x82, 0x83, 0x3A,
                    0x83, 0x77, 0x74, 0x7C, 0x3B
                };
                byte[] kCelestialWeapon =
                {
                    0x00, 0x50, 0x01, 0x00, 0x03, 0x00, 0xFF, 0x00, 0x11, 0x10,
                    0x03, 0x04, 0x24, 0x40, 0x19, 0x80, 0x0F, 0x80, 0x12, 0x80,
                    0x04, 0x80
                };
                bool[] celestialWeapon =
                {
                    false, false, false
                };
                byte[] yNameReset =
                {
                    0x68, 0x84, 0x7D, 0x70, 0x00
                };
                byte[] gearArray = new byte[22];
                byte[] kStartingWep =
                {
                    0x01, 0x50, 0x01, 0x00, 0x03, 0x00, 0x03, 0x00, 0x01, 0x0F,
                    0x03, 0x02, 0x1F, 0x40, 0x0B, 0x80, 0xFF, 0x00, 0xFF, 0x00,
                    0xFF, 0x00
                };
                byte[] kStartingArm =
                {
                    0x6E, 0x50, 0x01, 0x00, 0x03, 0x01, 0x03, 0x00, 0x01, 0x0F,
                    0x03, 0x02, 0x50, 0x40, 0x72, 0x80, 0xFF, 0x00, 0xFF, 0x00,
                    0xFF, 0x00
                };
                byte[] kChangeEquipped =
                {
                    0x60, 0x61
                };
                byte[] numOfOverdrivesReset =
                {
                    0xFF, 0xFF, 0xFF, 0xFF
                };
                byte[] writePartyFormation =
                {
                    0x00, 0x01, 0x03, 0x04, 0x05, 0x02, 0x06, 0xFF
                };

                #endregion

                #region "Reset Names, Party Members, and Equipment"

                Memory.WriteByteArray((IntPtr)(Functions.Offsets(20)), kNameReset);
                Memory.WriteByteArray((IntPtr)(Functions.Offsets(77)), yNameReset);
                Memory.WriteByte((IntPtr)(Functions.Offsets(46)), Dashboard.animaIsActuallyUnlocked);

                int ronsoRageTextPointer = Memory.ReadInt32((IntPtr)(Functions.Offsets(49)));
                ronsoRageTextPointer = (ronsoRageTextPointer + 0xA963);
                Memory.WriteByteArray((IntPtr)(ronsoRageTextPointer), ronsoRageReset);

                Memory.WriteByte((IntPtr)(Functions.Offsets(1)), 0x00);
                Memory.WriteByte((IntPtr)(Functions.Offsets(18)), 0x11);
                Memory.WriteByteArray((IntPtr)(Functions.Offsets(2)), numOfOverdrivesReset);

                Memory.WriteInt32((IntPtr)(Functions.Offsets(48)), 1);

                int storyProgress = Memory.ReadInt32((IntPtr)Functions.Offsets(94));

                if (storyProgress < 583)
                {
                    writePartyFormation[5] = 0xFF;
                    writePartyFormation[6] = 0xFF;
                }
                else if (storyProgress < 1085)
                {
                    writePartyFormation[6] = 0xFF;
                }

                Memory.WriteByteArray((IntPtr)((Functions.Offsets(44))), writePartyFormation);

                for (int i = 0; i < 115; i++)
                {
                    gearArray = Memory.ReadByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), 22);
                    if (gearArray[4] == 0x03)
                    {
                        gearArray[2] = 0x00;
                        gearArray[4] = 0x08;
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), gearArray);
                    }
                    else if (gearArray[4] == 0x07)
                    {
                        gearArray[2] = 0x00;
                        gearArray[4] = 0x08;
                        Memory.WriteByteArray((IntPtr)(Functions.Offsets(68) + (i * 22)), gearArray);
                    }
                }
                for (int i = 0; i < 25; i++)
                {
                    Memory.WriteByte((IntPtr)(Functions.Offsets(69) + (i * 22) + 6), 0xFF);
                    i++;
                }

                Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (24 * 22)), kStartingWep);
                Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (25 * 22)), kStartingArm);
                Memory.WriteByteArray((IntPtr)(Functions.Offsets(74)), kChangeEquipped);

                #endregion

                #region "Unlock Kimahri Celestial Weapon"

                int keyItems1 = Memory.ReadByte((IntPtr)(Functions.Offsets(80)));
                int keyItems2 = Memory.ReadByte((IntPtr)(Functions.Offsets(81)));
                if (((keyItems1 / 8) % 2) != 0)
                {
                    celestialWeapon[0] = true;
                }
                for (int i = 1; i < 32; i = i + 2)
                {
                    for (int n = 0; n < 16; n++)
                    {
                        if (keyItems2 == ((i * 8) + n))
                        {
                            celestialWeapon[1] = true;
                        }
                    }
                }
                for (int i = 1; i < 4; i = i + 2)
                {
                    for (int n = 0; n < 64; n++)
                    {
                        if (keyItems2 == ((i * 64) + n))
                        {
                            celestialWeapon[2] = true;
                        }
                    }
                }

                if ((celestialWeapon[0] == true) && (celestialWeapon[1] == true) && (celestialWeapon[2] == true))
                {
                    Memory.WriteByteArray((IntPtr)(Functions.Offsets(69) + (3 * 22)), kCelestialWeapon);
                }

                #endregion

                Dashboard.seymourModHasBeenOn = false;
            }
        }
    }
}

/*
 * The Sin Unleashed Project
 * */
