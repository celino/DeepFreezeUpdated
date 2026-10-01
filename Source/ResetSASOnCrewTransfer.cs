using UnityEngine;

namespace DeepFreeze
{


    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class ResetSASOnCrewTransfer : MonoBehaviour
    {
        public void Start()
        {
            GameEvents.onCrewTransferred.Add(OnCrewTransferred);
        }

        public void OnDestroy()
        {
            GameEvents.onCrewTransferred.Remove(OnCrewTransferred);
        }

        private void OnCrewTransferred(
            GameEvents.HostedFromToAction<ProtoCrewMember, Part> data)
        {
            Part destinationPart = data.to;

            if (destinationPart == null ||
                destinationPart.vessel == null)
                return;

            Vessel vessel = destinationPart.vessel;

            // Only do this if the transferred-to vessel is active.
            if (vessel != FlightGlobals.ActiveVessel)
                return;

            StartCoroutine(ResetSASNextFrame(vessel));
        }

        private System.Collections.IEnumerator ResetSASNextFrame(Vessel vessel)
        {
            // Let KSP finish rebuilding crew/control state.
            yield return null;

            if (vessel == null)
                yield break;

            bool sasWasOn =
                vessel.ActionGroups[KSPActionGroup.SAS];

            if (!sasWasOn)
                yield break;

            // Toggle SAS off.
            vessel.ActionGroups.SetGroup(
                KSPActionGroup.SAS,
                false);

            yield return null;

            // Turn SAS back on.
            vessel.ActionGroups.SetGroup(
                KSPActionGroup.SAS,
                true);

            // The stock behavior after SAS is re-enabled is
            // Stability Assist / attitude hold.
        }
    }
}