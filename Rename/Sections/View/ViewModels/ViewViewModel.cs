using Rename.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

//TODO: Implement slide show
//TODO: Restore current directory when re-opening the directory browser.
//      May have to stop showing the db from the view constructor, and do it from a button

namespace Rename.Sections.View.ViewModels
{

    public class ViewViewModel : ViewModelBase
    {

#region Members

        private readonly IInteractionManager _InteractionManager;
        private readonly INavigationManager _NavigationManager;

        private List<FileInfo> _Images;
        private string _CurrentDirectory;
        private string _CurrentFileName;
        private bool _IsBuildingImageList;
        private string _ImageFile;
        private int _ImageCount;
        private Random _Random;

#endregion Members

#region Constructors

        //------------------------------------------------------------------------------------------------------------------------
        public ViewViewModel(IInteractionManager interactionManager, INavigationManager navigationManager)
        {

            _InteractionManager = interactionManager;
            _NavigationManager = navigationManager;

            //this.ImageFile = @"c:\test\001.8509128c-4c1e-4f08-9998-edffd13757b6.jpg";
            //this.ImageFile = @"c:\test\002.b79dc969-8399-4863-a276-7ce9d569f62d.gif";
            //this.ImageFile = @"c:\test\003.3ff36fa7-a72c-4e4e-836c-58c352d9c6b3.png";
            //this.ImageFile = @"c:\test\012.2c01139b-9272-4fdf-ab58-9bc48adb46dd.bmp";

        }

#endregion Constructors

#region Properties

        //------------------------------------------------------------------------------------------------------------------------
        public string CurrentDirectory
        {
            get { return _CurrentDirectory; }
            set { base.SetProperty(ref _CurrentDirectory, value, () => this.CurrentDirectory); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public string CurrentFileName
        {
            get { return _CurrentFileName; }
            set { base.SetProperty(ref _CurrentFileName, value, () => this.CurrentFileName); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public bool IsBuildingImageList
        {
            get { return _IsBuildingImageList; }
            set { base.SetProperty(ref _IsBuildingImageList, value, () => this.IsBuildingImageList); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public string ImageFile
        {
            get { return _ImageFile; }
            set { base.SetProperty(ref _ImageFile, value, () => this.ImageFile); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public int ImageCount
        {
            get { return _ImageCount; }
            set { base.SetProperty(ref _ImageCount, value, () => this.ImageCount); }
        }

#endregion Properties

#region Commands

    #region Load Random Image

        //------------------------------------------------------------------------------------------------------------------------
        public ICommand LoadRandomImageCommand
        {
            get { return new RelayCommandEx(this.LoadRandomImageCommandExecute); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        private void LoadRandomImageCommandExecute()
        {

            try
            {
                LoadRandomImage();
            }
            catch (Exception ex)
            {
                _InteractionManager.DisplayError(ex);
            }

        }

    #endregion Load Random Image

#endregion Commands

#region Methods

        //------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Asynchronously calls method that builds internal list of displayable images in specified directory
        /// </summary>
        public async void BuildImageList(string directory)
        {

            this.CurrentDirectory = directory;
            try
            {
                this.IsBuildingImageList = true;
                if (!string.IsNullOrEmpty(directory))
                {
                    await Task.Run(() => BuildImageListAsync(directory));
                }
            }
            catch (Exception ex)
            {
                _InteractionManager.DisplayError(ex);
            }
            finally
            {
                this.IsBuildingImageList = false;
            }

        }

        //------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Builds internal list of images that exist in the specified directory
        /// </summary>
        private void BuildImageListAsync(string directory)
        {

            DirectoryInfo di = new DirectoryInfo(directory);
            _Images = new List<FileInfo>();

            foreach (string extension in new string[] { "jpg", "gif", "png", "bmp" })
            {
                FileInfo[] files = di.GetFiles("*." + extension);
                _Images.AddRange(files);
            }

            this.CurrentDirectory = directory;
            this.CurrentFileName = null;
            this.ImageCount = _Images.Count;
            _Random = new Random();

            LoadRandomImage();

        }

        //------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Picks a random image from the current directory and sets various properties associated with it
        /// </summary>
        private void LoadRandomImage()
        {
            if (_Images == null || !_Images.Any()) { return; }
            int randomNumber = _Random.Next(this.ImageCount);
            this.CurrentFileName = _Images[randomNumber].Name;
            this.ImageFile = _Images[randomNumber].FullName;
        }

#endregion Methods

    }

}
