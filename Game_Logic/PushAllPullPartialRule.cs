namespace PushPullBlocksGame
{

    public class PushAllPullPartialRule : MoveRule
    {
        public override void PushTop(Board board, Click click)
        {
            this.PushTopAll(board, click);
        }
        public override void PushDown(Board board, Click click)
        {
            this.PushDownAll(board, click);
        }
        public override void PushLeft(Board board, Click click)
        {
            this.PushLeftAll(board, click);
        }
        public override void PushRight(Board board, Click click)
        {
            this.PushRightAll(board, click);
        }
        public override void PullTop(Board board, Click click)
        {
            this.PullTopPartial(board, click);
        }
        public override void PullDown(Board board, Click click)
        {
            this.PullDownPartial(board, click);
        }
        public override void PullLeft(Board board, Click click)
        {
            this.PullLeftPartial(board, click);
        }
        public override void PullRight(Board board, Click click)
        {
            this.PullRightPartial(board, click);
        }

        //public override ActionResult Apply(Board board, Click click)
        //{
        //    return base.Apply(board, click);
        //}
    }
}