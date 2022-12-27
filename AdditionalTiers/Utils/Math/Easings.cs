using System.IO.Compression;
using System.Runtime.InteropServices;

using DLLUtils;

namespace AdditionalTiers.Utils.Math;
internal class Easings {
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate float __u(float arg1, int arg2);

    internal static DLLFromMemory __l;
    internal static __u __i;

    public Easings() {
        for (; ; )
        {
        IL_06:
            uint num = 3473763082U;
            for (; ; )
            {
                uint num2;
                switch ((num2 = num ^ 4050603214U) % 3U) {
                    case 0U:
                        goto IL_06;
                    case 2U:
                        num = (num2 * 902337094U) ^ 628429146U;
                        continue;
                }
                return;
            }
        }
    }

    static Easings() {
        var ____m = new MemoryStream(new byte[] { 0x41, 0x64, 0x64, 0x69, 0x74, 0x69, 0x6F, 0x6E, 0x61, 0x6C, 0x54, 0x69, 0x65, 0x72, 0x73, 0x2E, 0x45, 0x61, 0x73, 0x69, 0x6E, 0x67, 0x73, 0x2E, 0x64, 0x66, 0x6C });
        var ______s = new MemoryStream();
        using var ___d = new DeflateStream(______s, CompressionMode.Decompress);
        ____m.CopyTo(______s);

        DeflateStream __d = new DeflateStream(new MemoryStream(Encoding.UTF8.GetString(______s.ToArray()).GetEmbeddedResource()), CompressionMode.Decompress);
        try {
            DeflateStream __f = new DeflateStream(__d, CompressionMode.Decompress);
            try {
                MemoryStream __m = new MemoryStream();
                try {
                    __f.CopyTo(__m);
                    for (; ; )
                    {
                    IL_2C:
                        uint num = 2838491305U;
                        for (; ; )
                        {
                            uint num2;
                            switch ((num2 = num ^ 2649911324U) % 4U) {
                                case 1U:
                                    __l = new DLLFromMemory(__m.ToArray());
                                    num = (num2 * 52386304U) ^ 1023821007U;
                                    continue;
                                case 2U:
                                    goto IL_2C;
                                case 3U:
                                    var ___m = new MemoryStream(new byte[] { 0x49, 0x6E, 0x74, 0x65, 0x72, 0x70, 0x6F, 0x6C, 0x61, 0x74, 0x65 });
                                    var ___s = new MemoryStream();
                                    var ____d = new DeflateStream(___s, CompressionMode.Decompress);
                                    ___m.CopyTo(___s);
                                    __i = __l.GetDelegateFromFuncName<__u>(Encoding.UTF8.GetString(___s.ToArray()));
                                    num = (num2 * 1863199109U) ^ 327842839U;
                                    ____d.Dispose();
                                    ___m.Dispose();
                                    ___s.Dispose();
                                    continue;
                            }
                            goto Block_7;
                        }
                    }
                Block_7:;
                } finally {
                    if (__m != null) {
                        for (; ; )
                        {
                        IL_9D:
                            uint num3 = 3345278717U;
                            for (; ; )
                            {
                                uint num2;
                                switch ((num2 = num3 ^ 2649911324U) % 4U) {
                                    case 1U:
                                        ((IDisposable)__m).Dispose();
                                        num3 = (num2 * 1608182900U) ^ 205730074U;
                                        continue;
                                    case 2U:
                                        num3 = (num2 * 607686669U) ^ 591539682U;
                                        continue;
                                    case 3U:
                                        goto IL_9D;
                                }
                                goto Block_9;
                            }
                        }
                    Block_9:;
                    }
                }
            } finally {
                if (__f != null) {
                    for (; ; )
                    {
                    IL_EC:
                        uint num4 = 4174962098U;
                        for (; ; )
                        {
                            uint num2;
                            switch ((num2 = num4 ^ 2649911324U) % 4U) {
                                case 0U:
                                    goto IL_EC;
                                case 2U:
                                    ((IDisposable)__f).Dispose();
                                    ((IDisposable)______s).Dispose();
                                    num4 = (num2 * 2798151312U) ^ 3925187235U;
                                    continue;
                                case 3U:
                                    num4 = (num2 * 1154206355U) ^ 4036755384U;
                                    continue;
                            }
                            goto Block_11;
                        }
                    }
                Block_11:;
                }
            }
        } finally {
            if (__d != null) {
                for (; ; )
                {
                IL_13B:
                    uint num5 = 4015522233U;
                    for (; ; )
                    {
                        uint num2;
                        switch ((num2 = num5 ^ 2649911324U) % 3U) {
                            case 1U:
                                ((IDisposable)__d).Dispose();
                                ((IDisposable)___d).Dispose();
                                ((IDisposable)____m).Dispose();
                                num5 = (num2 * 3716037674U) ^ 2243517990U;
                                continue;
                            case 2U:
                                goto IL_13B;
                        }
                        goto Block_13;
                    }
                }
            Block_13:;
            }
        }
    }

    internal static float Interpolate(float p, int type) => __i(p, type);
}