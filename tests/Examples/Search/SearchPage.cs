using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Examples.Models;
using Nest;

namespace Examples.Search
{
	public class SearchPage : ExampleBase
	{
		[U]
		public void Line7()
		{
			// tag::9bdd3c0d47e60c8cfafc8109f9369922[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.QueryOnQueryString("tag:wow")
			);
			// end::9bdd3c0d47e60c8cfafc8109f9369922[]

			searchResponse.MatchesExample(@"GET /twitter/_search?q=tag:wow", e =>
			{
				e.Method = HttpMethod.POST;
				return e;
			});
		}

		[U]
		public void Line340()
		{
			// tag::be49260e1b3496c4feac38c56ebb0669[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.QueryOnQueryString("user:kimchy")
			);
			// end::be49260e1b3496c4feac38c56ebb0669[]

			searchResponse.MatchesExample(@"GET /twitter/_search?q=user:kimchy", e =>
			{
				e.Method = HttpMethod.POST;
				return e;
			});
		}

		[U]
		public void Line386()
		{
			// tag::f5569945024b9d664828693705c27c1a[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("kimchy,elasticsearch")
				.QueryOnQueryString("user:kimchy")
			);
			// end::f5569945024b9d664828693705c27c1a[]

			searchResponse.MatchesExample(@"GET /kimchy,elasticsearch/_search?q=user:kimchy", e =>
			{
				e.Method = HttpMethod.POST;
				return e;
			});
		}

		[U]
		public void Line398()
		{
			// tag::168bfdde773570cfc6dd3ab3574e413b[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index(Nest.Indices.AllIndices)
				.QueryOnQueryString("user:kimchy")
			);
			// end::168bfdde773570cfc6dd3ab3574e413b[]

			searchResponse.MatchesExample(@"GET /_search?q=user:kimchy", e =>
			{
				e.Method = HttpMethod.POST;
				return e;
			});
		}

		[U]
		public void Line407()
		{
			// tag::8022e6a690344035b6472a43a9d122e0[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index(Nest.Indices.All)
				.QueryOnQueryString("user:kimchy")
			);
			// end::8022e6a690344035b6472a43a9d122e0[]

			searchResponse.MatchesExample(@"GET /_all/_search?q=user:kimchy", e =>
			{
				e.Method = HttpMethod.POST;
				return e;
			});
		}

		[U]
		public void Line413()
		{
			// tag::43682666e1abcb14770c99f02eb26a0d[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("*")
				.QueryOnQueryString("user:kimchy")
			);
			// end::43682666e1abcb14770c99f02eb26a0d[]

			searchResponse.MatchesExample(@"GET /*/_search?q=user:kimchy", e =>
			{
				e.Method = HttpMethod.POST;
				return e;
			});
		}
	}
}