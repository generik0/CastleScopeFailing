namespace CastleScopeFailing
{
	public interface IScopeSomething
	{
		int Id { get; set; }
	}

	public class ScopeSomething : IScopeSomething
	{
		public int Id { get; set; }
	}
}