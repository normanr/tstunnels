namespace TSTunnels.Common.Messages
{
	public enum MessageType
	{
		Unknown = 0,
		HelloRequest,
		HelloResponse,
		ConnectRequest,
		ConnectResponse,
		ListenRequest,
		ListenResponse,
		AcceptRequest,
		StreamData,
		StreamError,
	}
}