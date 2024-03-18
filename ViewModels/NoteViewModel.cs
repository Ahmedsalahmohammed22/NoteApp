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

namespace NoteApp.ViewModels
{
    internal class NoteViewModel :INotifyPropertyChanged
    {
        //Fields
        private string _noteTitle;
        private string _noteDescription;
        private Note _selectedNote;

        //get & set
        public string NoteTitle
        {
            get => _noteTitle;
            set 
            { 
                if(_noteTitle != value)
                {
                    _noteTitle = value;
                    onPropertyChanged();
                }
            }
        }
        public string NoteDescription
        {
            get => _noteDescription;
            set
            {
                if (_noteDescription != value)
                {
                    _noteDescription = value;
                    onPropertyChanged();
                }
            }
        }
        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                if (_selectedNote != value)
                {
                    _selectedNote = value;
                    NoteTitle = _selectedNote.Title; 
                    NoteDescription = _selectedNote.Description;
                    onPropertyChanged();
                }
            }
        }

        //property
        public ObservableCollection<Note> NoteCollection { get; private set; }
        public ICommand AddNoteCommand { get; }
        public ICommand EditNoteCommand { get; }
        public ICommand RemoveNoteCommand { get; }

        public NoteViewModel()
        {
            NoteCollection = new ObservableCollection<Note>();
            AddNoteCommand = new Command(AddNote);
            RemoveNoteCommand = new Command(DeleteNote);
            EditNoteCommand = new Command(EditNote);
        }

        private void EditNote(object obj)
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

        private void DeleteNote(object obj)
        {
            if(SelectedNote != null)
            {
                NoteCollection.Remove(SelectedNote);
            }
            NoteTitle = string.Empty;
            NoteDescription = string.Empty;
        }

        private void AddNote(object obj)
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
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void onPropertyChanged([CallerMemberName]string? propertyName = null)
        {
            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(propertyName));
        }
    }
}
