using Microsoft.AspNetCore.Components;
using Sentience.Events;
using Blazored.Toast.Services;

namespace Sentience
{
    public class EventBase: ComponentBase
    {
        public static event EventHandler<GameLoadedArgs> GameLoaded;
        public static event EventHandler<HackEventArgs> HackUpdated;
        public static event EventHandler<JobEventArgs> JobUpdated;
        public static event EventHandler<ResearchEventArgs> ResearchUpdated;
        public static event EventHandler<ToastArgs> ToastUpdated;
        public static event EventHandler<UpgradeEventArgs> UpgradeUpdated;
        

        public void TriggerGameLoaded()
        {
            GameLoaded?.Invoke(this, new GameLoadedArgs {  Message = "Game Loaded"});
        }

        public void TriggerHackUpdate(string message)
        {
            HackUpdated?.Invoke(this, new HackEventArgs { Message = "Hack Updated" });
        }
        public void TriggerJobUpdate(string message)
        {
            JobUpdated?.Invoke(this, new JobEventArgs {  Message = "Job Updated"});
        }

        public void TriggerResearchUpdate(string message)
        {
            ResearchUpdated?.Invoke(this, new ResearchEventArgs { Message = "Research Updated" });
        }

        public void TriggerToast(string message, string heading, ToastLevel toastLevel)
        {
            
            ToastUpdated?.Invoke(this, new ToastArgs { Message = message, Heading = heading, ToastLevel = toastLevel });
        }

        public void TriggerUpgradeUpdate(string message)
        {
            UpgradeUpdated?.Invoke(this, new UpgradeEventArgs { Message = "Upgrade Updated" });
        }
    }
}
