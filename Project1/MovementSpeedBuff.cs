namespace Project1
{
    internal class MovementSpeedBuff : Buff
    {
        public MovementSpeedBuff()
        {
            Description = "Increase movement speed by 10%";
        }
        public override void Apply(Player player)
        {
            player.Speed *= 1.10f;
        }
    }
}
