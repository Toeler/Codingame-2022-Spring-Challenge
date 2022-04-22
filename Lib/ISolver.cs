using System.Collections.Generic;

namespace Lib {
	public interface ISolver<in TProblem, out TSolution> where TSolution : ISolution {
		IEnumerable<TSolution> GetSolutions(TProblem problem, Timer timer);
		string ShortName { get; }
	}
}
