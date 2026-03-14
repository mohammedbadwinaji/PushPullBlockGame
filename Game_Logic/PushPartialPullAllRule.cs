namespace PushPullBlocksGame
{

    public class PushPartialPullAllRule : MoveRule
    {
        public override void PushTop(Board board, Click click)
        {
            this.PushTopPartial(board, click);
        }
        public override void PushDown(Board board, Click click)
        {
            this.PushDownPartial(board, click);
        }
        public override void PushLeft(Board board, Click click)
        {
            this.PushLeftPartial(board, click);
        }
        public override void PushRight(Board board, Click click)
        {
            this.PushRightPartial(board, click);
        }
        public override void PullTop(Board board, Click click)
        {
            this.PullTopAll(board, click);
        }
        public override void PullDown(Board board, Click click)
        {
            this.PullDownAll(board, click);
        }
        public override void PullLeft(Board board, Click click)
        {
            this.PullLeftAll(board, click);
        }
        public override void PullRight(Board board, Click click)
        {
            this.PullRightAll(board, click);
        }

        //public override ActionResult Apply(Board board, Click click)
        //{
        //    return base.Apply(board, click);
        //}
    }
}