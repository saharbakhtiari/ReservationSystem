namespace Domain.FileManager
{
    public interface IFileFilter
    {
        public byte[] Filter(byte[] data);
    }
}
