namespace GM.Model
{
    internal interface IDataGrabber
    {
        void Grab();

        string Data { get; }
    }
}
