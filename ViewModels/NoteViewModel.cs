using CommunityToolkit.Mvvm.ComponentModel;
using NoteApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;



namespace NoteApp.ViewModels
{
    internal partial class NoteViewModel :ObservableObject
    {
        //Fields
        [ObservableProperty]
         string noteTitle;

        [ObservableProperty]
         string noteDescription;

        [ObservableProperty]
         Note selectedNote;
        [ObservableProperty]
        ObservableCollection<Note> noteCollection;


        public NoteViewModel()
        {
            NoteCollection = new ObservableCollection<Note>();
        }
        [RelayCommand]
        void EditNote()
        {
            if (SelectedNote != null)
            {
                foreach(Note note in NoteCollection.ToList())
                {
                    if(note == SelectedNote)
                    {
                        //Set New Note  
                        var newNote = new Note
                        {
                            Id = note.Id,
                            Title = NoteTitle,
                            Description = NoteDescription
                        };
                        //Remove Note
                        NoteCollection.Remove(note);
                        NoteCollection.Add(newNote);

                    }
                }
            }

        }
        [RelayCommand]
        void DeleteNote()
        {
            if(SelectedNote != null)
            {
                NoteCollection.Remove(SelectedNote);
            }
            NoteTitle = string.Empty;
            NoteDescription = string.Empty;
        }
        [RelayCommand]
        void AddNote()
        {
            var newId = NoteCollection.Count > 0 ? NoteCollection.Max(x => x.Id) + 1 : 1;
            var note = new Note
            {
                Title = NoteTitle,
                Description = NoteDescription,
            };
            NoteCollection.Add(note);
            //Rest Values
            NoteTitle = string.Empty;
            NoteDescription = string.Empty;
        }



        //PropertyChanged
        //public event PropertyChangedEventHandler? PropertyChanged;
        //protected virtual void OnPropertyChanged([CallerMemberName]string? propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(propertyName));
        //}
    }
}
