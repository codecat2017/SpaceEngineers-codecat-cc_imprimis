using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Sandbox.Common.ObjectBuilders;
using Sandbox.ModAPI;
using Sandbox.Definitions;
using Sandbox.Game.World;

using VRageMath;

using VRage.ModAPI;
using VRage.ObjectBuilders;
using VRage.Game.Components;
using VRage.Game;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Interfaces;
using IMyInventoryOwner = VRage.Game.ModAPI.Ingame.IMyInventoryOwner;
/*
 *  
 * Any trouble with this script should be reported to me "Inferi" on the mods workshop page " http://steamcommunity.com/sharedfiles/filedetails/?id=672194230 " 
 * 
 * 
 *  Used script made by Eikester http://steamcommunity.com/sharedfiles/filedetails/?id=677790017 as a tutorial for this.
 *  Check out Eikesters workshop for more great mods " http://steamcommunity.com/id/Eikester/myworkshopfiles/?appid=244850&p=1 "
 * 
 */
namespace cc_reactor_usage
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_Reactor))]
    public class cc_reactor_usage : MyGameLogicComponent
    {
        IMyReactor cc_Reactor;
        // Array med färger [START]
        private Color[] emissiveColors={
                                           new Color(0,0,0),
                                           new Color(98, 229, 0),
                                           new Color(115, 204, 0),
                                           new Color(132, 179, 0),
                                           new Color(150, 154, 0),
                                           new Color(167, 129, 0),
                                           new Color(185, 105, 0),
                                           new Color(202, 80, 0),
                                           new Color(220, 55, 0),
                                           new Color(237, 30, 0)
                                       };
        // Array med färger [END]
        public override MyObjectBuilder_EntityBase GetObjectBuilder(bool copy = false) {
            return cc_Reactor.GetObjectBuilder(copy);
        }
        public override void Init(MyObjectBuilder_EntityBase objectBuilder) {
            cc_Reactor = (IMyReactor)Entity;
        }
        public override void UpdateOnceBeforeFrame() {
            try {
                cc_Reactor.NeedsUpdate |= MyEntityUpdateEnum.EACH_100TH_FRAME;
            } catch {
            }
        }
        public override void UpdateBeforeSimulation100() {
            UpdateUsage();
        }
        void UpdateUsage(){
                if(cc_Reactor.BlockDefinition.SubtypeId == "cc_reactor_mk2_Large"){ // Om blocket är "cc_reactor_mk2_Large"
                    if(cc_Reactor.IsWorking && cc_Reactor.IsFunctional) { // Om blocket fungerar och är funktionsduglig
                        float maxReactorOutput = cc_Reactor.MaxOutput; // Hämta max Output för reaktorn
                        float currentReactorOutput = cc_Reactor.CurrentOutput * 100 / maxReactorOutput; // ta reda på procenten som används
                        // Sätt färger på Emissive efter procent som används
                        if(currentReactorOutput > 0) {
                            for(int i = -1; i <= 8; i++) {
                                if(currentReactorOutput > (i * 11.11)) {
                                    cc_Reactor.SetEmissiveParts("emissivePart" + i, emissiveColors[i + 1], 1f);
                                } else {
                                    cc_Reactor.SetEmissiveParts("emissivePart" + i, emissiveColors[0], 0f);
                                }
                            }
                        }  else {
                            for(int i = -1; i <= 8; i++) {
                                cc_Reactor.SetEmissiveParts("emissivePart" + i, emissiveColors[0], 0f);
                            }
                        }
                   }                    
                }
        }
        // Om jag vill visa ett meddelande [START]
        public static void ShowMessage(String message) {
            MyAPIGateway.Utilities.ShowNotification(message, 3000, MyFontEnum.Blue);
        }
        // Om jag vill visa ett meddelande [END]
    }
}
