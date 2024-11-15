namespace Project1
{
    internal class ShootSpeedBuff : Buff
    {
        public ShootSpeedBuff()
        {
            Description = "Decrease shoot cooldown by 10%";
        }

        public override void Apply(Player player)
        {
            player.ShootInterval *= 0.9f; //50% decrease
        }
    }
}
