using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOperation.Entities
{
    public class BaseEntity
    {
        public Guid OrgId { get; set; }

        public Guid DomainId { get; set; }

        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// This is use for MongoDb Id
        /// </summary>
      
        public object BsonId { get; set; }

        /// <summary>
        /// Gets or sets the GUID based entity identifier
        /// </summary>
        public Guid RecordId { get; set; }

        /// <summary>
        /// Gets or sets the Created Date time
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// Gets or sets the LastUpdated Date time
        /// </summary>
        public DateTime? LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the Created by
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the LastUpdated By
        /// </summary>
        public string LastUpdatedBy { get; set; }

        /// <summary>
        /// Get or Set IpAddress
        /// </summary>
        public string IpAddresss { get; set; }

        public int TotalRecord { get; set; }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

    }
}