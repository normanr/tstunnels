namespace TSTunnels.Common.Messages
{
	public enum MessageType
	{
		Unknown = 0,
		HelloRequest,
		HelloReponse,
		ConnectRequest,
		ConnectResponse,
		ListenRequest,
		ListenResponse,
		StreamData,
		StreamError,
	}
}