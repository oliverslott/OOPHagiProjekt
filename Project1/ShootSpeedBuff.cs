namespace Project1
{
    internal class ShootSpeedBuff : Buff
    {
        public ShootSpeedBuff()
        {
            Description = "Decrease shoot cooldown by 50%";
        }

        public override void Apply(Player player)
        {
            player.ShootInterval *= 0.5f; //50% decrease
        }
    }
}
