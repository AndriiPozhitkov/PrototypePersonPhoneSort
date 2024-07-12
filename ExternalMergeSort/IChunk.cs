
namespace ExternalMergeSort
{
    public interface IChunk
    {
        bool NotEmpty();
        void Sort();
        Task Write(IWriter writer);
    }
}