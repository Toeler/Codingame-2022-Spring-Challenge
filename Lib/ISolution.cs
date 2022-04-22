namespace Lib {
	public interface ISolution
	{
		double Score { get; }
		SolutionDebugInfo DebugInfo { get; set; }
	}
}
