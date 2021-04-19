using ElectronicsANC.Models;
using ElectronicsANC.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Repository
{
    public class MemberRepository
    {
        public ElectronicsDbDataContext dbContext;

        public MemberRepository()
        {
            dbContext = new ElectronicsDbDataContext();
        }

        public MemberRepository(ElectronicsDbDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<MemberModel> GetAllMembers()
        {
            List<MemberModel> memberList = new List<MemberModel>();

            foreach (Member dbMember in dbContext.Members)
                AddDbObjectToModelCollection(memberList, dbMember);

            return memberList;
        }

        public MemberModel GetMemberById(Guid id)
        {
            var member = dbContext.Members.FirstOrDefault(x => x.IdMember == id);

            return MapDbObjectToModel(member);
        }

        public List<MemberModel> GetMembersByCity(string city)
        {
            List<MemberModel> memberList = new List<MemberModel>();

            foreach (Member dbMbember in dbContext.Members)
                if (dbMbember.City.Equals(city))
                    AddDbObjectToModelCollection(memberList, dbMbember);

            return memberList;
        }

        public MemberModel GetMemberByPhone(string phone)
        {
            var member = dbContext.Members.FirstOrDefault(x => x.Phone.Equals(phone));

            return MapDbObjectToModel(member);
        }

        public MemberModel GetMemberByEmail(string email)
        {
            var member = dbContext.Members.FirstOrDefault(x => x.Email.Equals(email));

            return MapDbObjectToModel(member);
        }

        public void InsertMember(MemberModel member)
        {
            member.IdMember = Guid.NewGuid();

            dbContext.Members.InsertOnSubmit(MapModelToDbObject(member));
            dbContext.SubmitChanges();
        }

        public void UpdateMember(MemberModel member)
        {
            Member dbMember = dbContext.Members.FirstOrDefault(x => x.IdMember == member.IdMember);

            if(dbMember != null)
            {
                dbMember.IdMember = member.IdMember;
                dbMember.FirstName = member.FirstName;
                dbMember.LastName = member.LastName;
                dbMember.Address = member.Address;
                dbMember.City = member.City;
                dbMember.Country = member.Country;
                dbMember.PostalCode = member.PostalCode;
                dbMember.Phone = member.Phone;
                dbMember.Email = member.Email;
                dbContext.SubmitChanges();
            }
        }

        public void DeleteMember(Guid id)
        {
            Member dbMember = dbContext.Members.FirstOrDefault(x => x.IdMember == id);

            if(dbMember != null)
            {
                dbContext.Members.DeleteOnSubmit(dbMember);
                dbContext.SubmitChanges();
            }
        }

        private void AddDbObjectToModelCollection(List<MemberModel> memberList, Member dbMember)
        {
            memberList.Add(MapDbObjectToModel(dbMember));
        }

        private MemberModel MapDbObjectToModel(Member dbMember)
        {
            MemberModel member = new MemberModel();

            if(dbMember != null)
            {
                member.IdMember = dbMember.IdMember;
                member.FirstName = dbMember.FirstName;
                member.LastName = dbMember.LastName;
                member.Address = dbMember.Address;
                member.City = dbMember.City;
                member.Country = dbMember.Country;
                member.PostalCode = dbMember.PostalCode;
                member.Phone = dbMember.Phone;
                member.Email = dbMember.Email;

                return member;
            }

            return null;
        }

        private Member MapModelToDbObject(MemberModel member)
        {
            Member dbMember = new Member();

            if(member != null)
            {
                dbMember.IdMember = member.IdMember;
                dbMember.FirstName = member.FirstName;
                dbMember.LastName = member.LastName;
                dbMember.Address = member.Address;
                dbMember.City = member.City;
                dbMember.Country = member.Country;
                dbMember.PostalCode = member.PostalCode;
                dbMember.Phone = member.Phone;
                dbMember.Email = member.Email;

                return dbMember;
            }

            return null;
        }
    }
}