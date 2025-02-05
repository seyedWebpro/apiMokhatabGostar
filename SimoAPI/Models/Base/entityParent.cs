using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


[Serializable]
public abstract class entityParent
{
    public DateTime? createTime { get; set; }

    public DateTime? editeTime { get; set; }

    public long? createdBy { get; set; }

    public long? editedBy { get; set; }

    public string? rempoteIp { get; set; }

    public string? setion { get; set; }

    //[NotMapped()] //امکان درج آیدی دلخواه
    public byte allowId { get; set; } = 0;

    public abstract void requiredValidation(ref serviceresult result);

    public abstract void uniqueValidation(ref serviceresult result, apiContext context);

    public abstract void deleteValidation(ref serviceresult result, apiContext context);

}

