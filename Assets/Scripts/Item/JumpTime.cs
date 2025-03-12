namespace Item
{
    public static class JumpTime
    {
        public static bool jump { get; private set; }

        public static bool hasGotItem { get; private set; }
        public static void StartJump() => jump = true;

        public static void StopJump() => jump = false;
        
        public static void GotItem() => hasGotItem = true;
        
        public static void GaveItem() => hasGotItem = false;
    }
}
