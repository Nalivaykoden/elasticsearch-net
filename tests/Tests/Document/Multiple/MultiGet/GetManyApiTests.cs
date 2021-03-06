﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client.Settings;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;

namespace Tests.Document.Multiple.MultiGet
{
	public class GetManyApiTests : IClusterFixture<ReadOnlyCluster>
	{
		private readonly IElasticClient _client;
		private readonly IEnumerable<long> _ids = Developer.Developers.Select(d => d.Id).Take(10);

		public GetManyApiTests(ReadOnlyCluster cluster) => _client = cluster.Client;

		[I] public void UsesDefaultIndexAndInferredType()
		{
			var response = _client.GetMany<Developer>(_ids);
			response.Count().Should().Be(10);
			foreach (var hit in response)
			{
				hit.Index.Should().NotBeNullOrWhiteSpace();
				hit.Id.Should().NotBeNullOrWhiteSpace();
				hit.Found.Should().BeTrue();
			}
		}

		[I] public async Task UsesDefaultIndexAndInferredTypeAsync()
		{
			var response = await _client.GetManyAsync<Developer>(_ids);
			response.Count().Should().Be(10);
			foreach (var hit in response)
			{
				hit.Index.Should().NotBeNullOrWhiteSpace();
				hit.Id.Should().NotBeNullOrWhiteSpace();
				hit.Found.Should().BeTrue();
			}
		}

		[I] public async Task CanHandleNotFoundResponses()
		{
			var response = await _client.GetManyAsync<Developer>(_ids.Select(i => i * 100));
			response.Count().Should().Be(10);
			foreach (var hit in response)
			{
				hit.Index.Should().NotBeNullOrWhiteSpace();
				hit.Id.Should().NotBeNullOrWhiteSpace();
				hit.Found.Should().BeFalse();
			}
		}

		[I] public void ThrowsExceptionOnConnectionError()
		{
			if (TestConnectionSettings.RunningFiddler) return; //fiddler meddles here

			var client = new ElasticClient(new TestConnectionSettings(port: 9500));
			Action response = () => client.GetMany<Developer>(_ids.Select(i => i * 100));
			response.Should().Throw<ElasticsearchClientException>();
		}
	}
}
