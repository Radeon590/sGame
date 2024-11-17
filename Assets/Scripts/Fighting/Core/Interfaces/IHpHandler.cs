namespace Fighting.Core
{
    public interface IHpHandler
    {
        public float Hp { get; }
        public void HandleDamage(float damage);
    }
}