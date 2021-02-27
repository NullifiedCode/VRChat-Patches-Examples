using System;
using System.Reflection;
using System.Linq;

namespace PatchExamplesVRChat
{
    /* Created by NullifiedCode */
    /* Feel free to copy and paste this avatar struct to any project and use it i dont really care about it i was just tired
     * of dealing with the normal avatar api thing.
     * <3
     * 
     * PS: I did not include the using < BLANK > up top because im pretty sure you can figure that one out.
     * 
     */
    class _Avatar
    {
        public _Avatar(VRCPlayer p, GameObject g)
        {
            if (g != null)
            {
                avatarObject = g;
                gameObject = g;
                blueprintID = g.GetComponent<PipelineManager>().blueprintId;
                releaseStatus = p.prop_ApiAvatar_0.releaseStatus;
                release = p.prop_ApiAvatar_0.releaseStatus;
                avatarId = g.GetComponent<PipelineManager>().blueprintId;
                avatarPackage = p.prop_ApiAvatar_0.unityPackageUrl;
                avatarUrl = p.prop_ApiAvatar_0.unityPackageUrl;
                avatarImage = p.prop_ApiAvatar_0.imageUrl;
                avatarName = p.prop_ApiAvatar_0.name;
                avatarAuthor = p.prop_ApiAvatar_0.authorName;
                avatarAuthorId = p.prop_ApiAvatar_0.authorId;
                vrcplayer = p;
                apiuser = p.field_Private_APIUser_0;
            }
        }
        public _Avatar(VRC.Player p, GameObject g)
        {
            if (g != null)
            {
                avatarObject = g;
                gameObject = g;
                blueprintID = g.GetComponent<PipelineManager>().blueprintId;
                releaseStatus = p.prop_ApiAvatar_0.releaseStatus;
                release = p.prop_ApiAvatar_0.releaseStatus;
                avatarId = g.GetComponent<PipelineManager>().blueprintId;
                avatarPackage = p.prop_ApiAvatar_0.unityPackageUrl;
                avatarUrl = p.prop_ApiAvatar_0.unityPackageUrl;
                avatarImage = p.prop_ApiAvatar_0.imageUrl;
                avatarName = p.prop_ApiAvatar_0.name;
                avatarAuthor = p.prop_ApiAvatar_0.authorName;
                avatarAuthorId = p.prop_ApiAvatar_0.authorId;
                vrcplayer = p.field_Internal_VRCPlayer_0;
                apiuser = p.field_Private_APIUser_0;
            }
        }
        public GameObject avatarObject = null, gameObject = null;
        public string blueprintID = null;
        public string releaseStatus = null;
        public string release = null;
        public string avatarId = null;
        public string avatarPackage = null;
        public string avatarUrl = null;
        public string avatarImage = null;
        public string avatarName = null;
        public string avatarAuthor = null;
        public string avatarAuthorId = null;
        public APIUser apiuser = null;
        public VRCPlayer vrcplayer = null;
    }
    public class Patches
    {
        /* I just want to say that this function & Patch below is from DynamicBonesSaftey https://github.com/BenjaminZehowlt/DynamicBonesSafety/blob/master/DynamicBonesSafetyMod.cs#L72 */
        private static bool checkXref(MethodBase m, string match)
        {
            try
            {
                return XrefScanner.XrefScan(m).Any(
                    instance => instance.Type == XrefType.Global && instance.ReadAsObject() != null && instance.ReadAsObject().ToString().ToLower()
                                   .Equals(match.ToLower(), StringComparison.OrdinalIgnoreCase));
            }
            catch { } // ignored

            return false;
        }

        private static HarmonyMethod GetLocalPatch(string name) { return new HarmonyMethod(typeof(Patches).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic)); }
        public static void Examples()
        {
            /* Thanks Slay <3 */
            new gPatch("AntiDownload", typeof(AssetBundleDownloadManager).GetMethod("Method_Internal_Void_ApiAvatar_MulticastDelegateNInternalSealedVoUnUnique_MulticastDelegateNInternalSealedVoObUnique_MulticastDelegateNInternalSealedVoStObStUnique_Boolean_EnumNInternalSealedva3vUnique_0"), GetLocalPatch("DownloadPatch"), null).AttemptPatch();

            /* Thanks DynamicBonesSaftey */
            typeof(VRCPlayer).GetMethods().Where(m => m.Name.StartsWith("Method_Private_Void_GameObject_VRC_AvatarDescriptor_Boolean_") && !checkXref(m, "Avatar is Ready, Initializing")).ToList()
             .ForEach(m => new gPatch("AvaLoad", typeof(VRCPlayer).GetMethod(m.Name), GetLocalPatch("AvaLoaded"), null).AttemptPatch());
        }

        private static bool DownloadPatch(ref ApiAvatar __0, ref MulticastDelegateNInternalSealedVoUnUnique __1, ref MulticastDelegateNInternalSealedVoObUnique __2, ref MulticastDelegateNInternalSealedVoStObStUnique __3, ref bool __4, ref EnumNInternalSealedva3vUnique __5)
        {
            if(__0.id == "avtr_5e4b50ff-0b05-4ed2-97e4-c6040af98f53")
            {
                MelonLogger.Msg($"{__0.id} That is a very bad crasher blocked....");
                return false;
            }
            MelonLogger.Msg(__0.id);    

            /*
             * __0 = ApiAvatar
             * __1 = On Download Progress
             * __2 = On Download Success
             * __3 = On Download Error
             * __4 = Bypass Size Limit
             * __5 = UnpackType
             * Returning false will block the avatar download.
             * Returning true will allow the avatar to download.
             */

            return true;
        }

        private static void AvaLoaded(VRCPlayer __instance, GameObject __0, VRC_AvatarDescriptor __1, bool __2)
        {
            if (__instance == null || __0 == null || __1 == null || !__2)
                return;

            _Avatar ava = new _Avatar(__instance, __0);


            /* Ava contains many usful things for developers id,asseturl,author,releasestatus,etc */

            MelonLogger.Msg(ava.avatarId);
        }
    }
}
