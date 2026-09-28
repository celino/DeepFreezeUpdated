using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ToolbarControl_NS;

namespace DeepFreeze
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class RegisterToolbar : MonoBehaviour
    {
        public const string MODID = "DeepFreeze";
        public const string MODNAME = "DeepFreeze";


        void Start()
        {
            ToolbarControl.RegisterMod(MODID, MODNAME);
#if false
            Log = new KSP_Log.Log("MissionPlanner"
#if DEBUG
                , KSP_Log.Log.LEVEL.DETAIL
#endif
                    );
#endif
        }
    }
}