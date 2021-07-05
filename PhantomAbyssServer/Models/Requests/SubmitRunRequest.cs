using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhantomAbyssServer.Database.Models;

namespace PhantomAbyssServer.Models.Requests
{
    public class SubmitRunRequest : IValidatableObject
    {
        [Required]
        public UserCurrency Currency { get; set; }
        public uint DungeonFloorNumber { get; set; }
        public uint DungeonId { get; set; }
        public uint RouteId { get; set; }
        public List<uint> LastLockedDungeonList { get; set; }
        public string PermanentSettingsData { get; set; }
        public uint PlayerUniqueId { get; set; }
        public string RunData { get; set; }
        public bool Success { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Success && string.IsNullOrEmpty(RunData))
            {
                yield return new ValidationResult("Run is successful but run data was not specified", new[] {nameof(RunData)});
            }
        }
    }
}