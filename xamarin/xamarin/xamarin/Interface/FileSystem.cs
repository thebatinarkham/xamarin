using PCLStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace xamarin.Interface
{
    public static class FileSystem
    {
        public static IFileSystem Current { get; }
    }

    public interface IFileSystem
    {
        IFolder LocalStorage { get; }
        IFolder RoamingStorage { get; }

        Task<IFile> GetFileFromPathAsync(string path);
        Task<IFolder> GetFolderFromPathAsync(string path);
    }

    public enum CreationCollisionOption
    {
        GenerateUniqueName = 0,
        ReplaceExisting = 1,
        FailIfExists = 2,
        OpenIfExists = 3,
    }

    public interface IFolder
    {
        string Name { get; }
        string Path { get; }

        Task<IFile> CreateFileAsync(string desiredName, CreationCollisionOption option);
        Task<IFile> GetFileAsync(string name);
        Task<IList<IFile>> GetFilesAsync();

        Task<IFolder> CreateFolderAsync(string desiredName,
            CreationCollisionOption option);
        Task<IFolder> GetFolderAsync(string name);
        Task<IList<IFolder>> GetFoldersAsync();

        Task<ExistenceCheckResult> CheckExistsAsync(string name,
            CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteAsync();
    }

    public enum FileAccess
    {
        Read,
        ReadAndWrite
    }

    public interface IFile
    {
        string Name { get; }
        string Path { get; }

        Task<Stream> OpenAsync(FileAccess fileAccess);
        Task DeleteAsync();
        Task RenameAsync(string newName,
          NameCollisionOption collisionOption = NameCollisionOption.FailIfExists,
          CancellationToken cancellationToken = default(CancellationToken));
        Task MoveAsync(string newPath,
          NameCollisionOption collisionOption = NameCollisionOption.ReplaceExisting,
          CancellationToken cancellationToken = default(CancellationToken));
    }

    
}
