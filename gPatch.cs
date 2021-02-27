using System;
using System.Collections.Generic;
using System.Text;

namespace PatchExamplesVRChat
{
    class gPatch
    {
        public string ID { get; set; }
        public MethodBase TargetMethod { get; set; }
        public HarmonyMethod Prefix { get; set; }
        public HarmonyMethod Postfix { get; set; }
        public gPatch(string Identifier, MethodBase Target, HarmonyMethod Before, HarmonyMethod After)
        {
            ID = Identifier;
            TargetMethod = Target;
            Prefix = Before;
            Postfix = After;
        }
        public void AttemptPatch()
        {
            try
            {
                HarmonyInstance instance = HarmonyInstance.Create(ID);
                instance.Patch(TargetMethod, Prefix, Postfix);
            }
            catch (Exception ex)
            {
                /* Do somehing here */
            }
        }
    }
}
