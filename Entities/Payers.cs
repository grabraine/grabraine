namespace WebApi.Entities
{
    public class Payers
    {
        public int Id { get; set; }
        public string Name {get; set;}
        public string Type {get; set;}
        public string Phone_1 {get; set;}
        public string Phone_2 {get; set;}
        public string Email {get; set;}
        public string Fax {get; set;}
        public string PayerId {get; set;}
        public string Notes {get; set;}
        public string SystemNoteKey {get; set;}
        public string Address_1 {get; set;}
        public string Address_2 {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public string Zip {get; set;}
    }
}