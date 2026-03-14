namespace PushPullBlocksGame
{

    public class ActionResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        
        public ActionResult(bool Success,string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        public static ActionResult Fail(string message)
        {
            return new ActionResult( false, message);
        }

        public static ActionResult Ok(string Message)
        {
            return new ActionResult(true, Message); 
        }
    }
}