namespace Connect4.GameServer
{
	public class BoardPosition
	{
		public BoardPosition()
		{
			State = PositionState.Empty; //ensures when position is declared that it is marked as empty
		}

		public PositionState State { get; set; }

		public bool IsYellow()
		{
			return State == PositionState.Yellow;
		}

		public bool IsRed()
		{
			return State == PositionState.Red;
		}

		public bool IsEmpty()
		{
			return State == PositionState.Empty;
		}
	}

	public enum PositionState
	{
		Empty = 0,  //3 values that are needed for connect 4
		Yellow = 1,
		Red = 2,
	}
}
