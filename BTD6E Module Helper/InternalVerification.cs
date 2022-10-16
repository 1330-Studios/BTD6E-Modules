namespace BTD6E_Module_Helper;
public sealed class InternalVerification {
    public static MelonLogger.Instance Logger = new("BTD6E Module Helper");

    public static void Verify() {
        Assembly anhetiohanj = Assembly.GetCallingAssembly();
        string ihntkekolan = anhetiohanj.GetName().Name;
        var amltkmeoapslm = HashHelper.Create();
        var ajhieotrjs = Aes.Create();
        int nuaa45z65423m3 = 0, a3d2z16v47 = nuaa45z65423m3, a4156f4zd56zf4esd_Z = a3d2z16v47;
        string zxvcfgt4220 = "", iut__282jhis = zxvcfgt4220, agxjnigps9s0 = iut__282jhis, afajed90tgsj09 = agxjnigps9s0, jikorjtu890us08 = afajed90tgsj09, zniodf9s90g87u09drs = jikorjtu890us08;
        string[] afhd8f90as___SJjjj = new[] { zxvcfgt4220 };
        unsafe {
            for (; ; ) {
            up:
                uint agtesbnuognsc_________________true = 2838269950U;
                for (; ; ) {
                    uint adbduiofh__mmbn_false;
                    switch ((adbduiofh__mmbn_false = agtesbnuognsc_________________true ^ 2852052160U) % 20U) {
                        case 0U: {
                                agtesbnuognsc_________________true = adbduiofh__mmbn_false * 246926312U ^ 4102249799U;
                                continue;
                            }
                        case 2U: {
                                jikorjtu890us08 = string.Join("", amltkmeoapslm.ComputeHash(File.ReadAllBytes(zxvcfgt4220)));
                                agtesbnuognsc_________________true = (jikorjtu890us08.Equals("48521775232421182423125511420624314814206") ? 4223158665U : 2391898859U) ^ adbduiofh__mmbn_false * 3318631163U;
                                continue;
                            }
                        case 3U:
                            agtesbnuognsc_________________true = adbduiofh__mmbn_false * 2253409599U ^ 3459944374U;
                            continue;
                        case 4U:
                            a3d2z16v47++;
                            agtesbnuognsc_________________true = adbduiofh__mmbn_false * 3239030374U ^ 1728964762U;
                            continue;
                        case 5U:
                            goto up;
                        case 6U: {
                                var a = Equals(a4156f4zd56zf4esd_Z, Environment.NewLine);
                                agtesbnuognsc_________________true = (adbduiofh__mmbn_false * 4178722261U ^ 580650086U) + (a ? 15550354U : 0U);
                                continue;
                            }
                        case 7U: {
                                agtesbnuognsc_________________true = (a4156f4zd56zf4esd_Z > 0) ? 3492063402U : 2607290964U;
                                continue;
                            }
                        case 8U:
                            agtesbnuognsc_________________true = (anhetiohanj.IsDynamic ? 1813943069U : 828738791U) ^ adbduiofh__mmbn_false * 3121744999U;
                            continue;
                        case 9U:
                            agtesbnuognsc_________________true = ((!anhetiohanj.FullName.Reverse().Equals("pleH_BA")) ? 3540758650U : 3672858079U) ^ adbduiofh__mmbn_false * 1342339521U;
                            continue;
                        case 10U: {
                                agtesbnuognsc_________________true = ((ihntkekolan != "Verification") ? 1362718011U : 1044279093U) ^ adbduiofh__mmbn_false * 3411289605U;
                                continue;
                            }
                        case 11U: {
                                Logger.Msg("v" + zniodf9s90g87u09drs);
                                agtesbnuognsc_________________true = adbduiofh__mmbn_false * 813152480U ^ 697063549U;
                                continue;
                            }
                        case 12U: {
                                afhd8f90as___SJjjj = Directory.GetFiles(agxjnigps9s0);
                                agtesbnuognsc_________________true = adbduiofh__mmbn_false * 1384865188U ^ 3874908648U;
                                continue;
                            }
                        case 13U:
                            agtesbnuognsc_________________true = anhetiohanj.Location.IsNormalized() ? 3796375160U : 3382295791U;
                            continue;
                        case 14U: {
                                a4156f4zd56zf4esd_Z = checked(amltkmeoapslm.ComputeHash(Encoding.UTF32.GetBytes(Environment.CommandLine))[0] % 5 + amltkmeoapslm.ComputeHash((from a in ajhieotrjs.LegalKeySizes
                                                                                                                                                                              select (byte)a.MaxSize).ToArray())[3] + 48745) ^ 451;
                                agtesbnuognsc_________________true = 3382295791U;
                                continue;
                            }
                        case 15U: {
                                a3d2z16v47 = new NKR().Next() ^ 330;
                                agtesbnuognsc_________________true = adbduiofh__mmbn_false * 259682229U ^ 2582466770U;
                                continue;
                            }
                        case 16U: {
                                zniodf9s90g87u09drs = anhetiohanj.GetCustomAttribute<MelonInfoAttribute>().Version;
                                afajed90tgsj09 = Path.Combine(Environment.CurrentDirectory, "BloonsTD6_Data", "Plugins");
                                agxjnigps9s0 = Path.Combine(afajed90tgsj09, "x86_64");
                                agtesbnuognsc_________________true = 3320625984U;
                                continue;
                            }
                        case 17U: {
                                nuaa45z65423m3++;
                                agtesbnuognsc_________________true = 4096194895U;
                                continue;
                            }
                        case 18U: {
                                zxvcfgt4220 = afhd8f90as___SJjjj[nuaa45z65423m3];
                                agtesbnuognsc_________________true = 4233198212U;
                                continue;
                            }
                        case 19U: {
                                agtesbnuognsc_________________true = (nuaa45z65423m3 >= afhd8f90as___SJjjj.Length) ? 3315697637U : 2591865566U;
                                continue;
                            }
                    }
                    return;
                }
            }
        }
    }
}