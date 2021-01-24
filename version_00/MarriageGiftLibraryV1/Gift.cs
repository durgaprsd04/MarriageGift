using System;

namespace MarriageGiftLibraryV1
{
    public class Gift
    {
        private Guid id;
        private string name;
        public Gift(string name)
        {
            this.id = Guid.NewGuid();
            this.name =name;
        }
        public override string ToString()
        {
            return $"Gift {id}:{name}";
        }
    }
}
