using Framework;

namespace Gameplay
{
    public sealed class VictoryMoney : Singleton<VictoryMoney>
    {
        public int Money { get; set; }
        
        protected override void Awake()
        {
            base.Awake();
            CanDestroyOnLoad = false;
        }
    }
}