using System;

namespace Service.DTO
{
    public class ClientRes
    {
        public int ClientId { get; set; }
        public string IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string FullNameWithPhone1 { get; set; }
        public int? KupatHolimId { get; set; }
        public int? ClientTypeId { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
    }

    public class ClientForSearchRes
    {
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string FullNameWithPhone1 { get; set; }
        public int? KupatHolimId { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public bool IsActiveSubs { get; set; }
        public int? VisitsLeft { get; set; }
        public int? Balance { get; set; }
        public int? SubsId{ get; set; }
    }
    public class ClientForAddManuall
    {
        public int ClientId { get; set; }
        public string ClientFullName { get; set; }
        public int? SubscriptionId { get; set; }
        public string Result { get; set; }
        public int? SubStatus { get; set; }


        public DateTime? SubsStartDate { get; set; }
        public DateTime? SubsEndDate { get; set; }
        public string ProgramName { get; set; }
        public int? VisitLeft { get; set; }
        public int? Balance { get; set; }
    }

    public class ClientCreate
    {
        public string IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? KupatHolimId { get; set; }
        public int? ClientTypeId { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }

    }


    public class ClientUpdate : ClientCreate
    {
        public int ClientId { get; set; }
    }

    public class ClientInLessonRes
    {
        public int ClientId { get; set; }
        public string ClientFullName { get; set; }
        public DateTime SubsStartDate { get; set; }
        public DateTime SubsEndDate { get; set; }
        public DateTime JoinDate { get; set; }
        public int? SubscriptionId { get; set; }
        public string ProgramName { get; set; }
        public int VisitLeft { get; set; }
        public int? Balance { get; set; }
        public bool IsVisitToday { get; set; }
        public int SubsStatusId { get; set; }
        public string SubsStatusStyle { get; set; }
        public string SubsStatusLabel { get; set; }

    }

    public class ClientToLessonCreate
    {
        public int ClientId { get; set; }
        public int LessonId { get; set; }
    }

    public class ClientFileCreate
    {
        public int ClientId { get; set; }
        public int FileTypeId { get; set; }
        public string FileAsBase { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Comment { get; set; }
    }

    public class ClientFileNamesRes
    {
        public string ActionDate { get; set; }
        public int ClientFileId { get; set; }
        public int ClientId { get; set; }
        public int FileTypeId { get; set; }
        public string FileTypeDesc { get; set; }
        public string FileName { get; set; }
        public string Comment { get; set; }
    }

    public class ClientFileRes
    {
        public byte[] File { get; set; }
        public string FileName{ get; set; }
    }
}

