﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class FollowInfoResponse : ResponseBase
	{
		[DataMember(Name = "follower_indices")]
		public IReadOnlyCollection<FollowerInfo> FollowerIndices { get; internal set; } = EmptyReadOnly<FollowerInfo>.Collection;
	}

	public class FollowerInfo
	{
		[DataMember(Name = "follower_index")]
		public string FollowerIndex { get; internal set; }

		[DataMember(Name = "remote_cluster")]
		public string RemoteCluster { get; internal set; }

		[DataMember(Name = "leader_index")]
		public string LeaderIndex { get; internal set; }

		[DataMember(Name = "status")]
		public FollowerIndexStatus Status { get; internal set; }

		[DataMember(Name = "parameters")]
		public FollowConfig Parameters { get; internal set; }
	}
}
