using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Exigo_DockerManager.Models.DockerFileViewModel
{
    public class DockerFile
    {
        public int DockerFileId { get; set; }
        [Required]
        [Display(Name = "Tag")]
        public string Tag { get; set; }
        [Display(Name = "Content")]
        [DataType(DataType.MultilineText)]
        [UIHint("DockerFileContent")]
        public string Content { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Build> Builds { get; set; }
    }
    public class Build
    {
        public int BuildId { get; set; }
        public int DockerFileId { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateComplete { get; set; }
        public string DockerFileContent { get; set; }
        public string ResultingImageId {get;set;}
        public string Tags { get; set; }
        
    }
    

}