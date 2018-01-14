using Orleans.Concurrency;
using ProtoBuf;

namespace BitEx.Core.Result
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [Immutable]
    public struct Result<O>
    {
        public bool IsOk { get; set; }
        public O Data { get; set; }
        public Error Err { get; set; }
        static Result<O> okObj = new Result<O> { IsOk = true, Data = default, Err = default };
        public static Result<O> Ok()
        {
            return okObj;
        }
        public static Result<O> Ok(O ok)
        {
            return new Result<O> { IsOk = true, Data = ok, Err = default };
        }
        public static Result<O> Error(int code, string Message)
        {
            return new Result<O> { IsOk = false, Data = default, Err = new Error { Code = code, Message = Message } };
        }
        public static Result<O> Error(int code, string Message, O data)
        {
            return new Result<O> { IsOk = false, Data = data, Err = new Error { Code = code, Message = Message } };
        }
    }
}
