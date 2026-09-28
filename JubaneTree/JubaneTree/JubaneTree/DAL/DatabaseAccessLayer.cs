using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JubaneTree.Models;

namespace JubaneTree.DAL
{
    public class DatabaseAccessLayer
    {
        private readonly string _connectionString =
            ConfigurationManager
                .ConnectionStrings["DefaultConnection"]
                .ConnectionString;


        #region Helpers

        /// <summary>
        /// Converts a null C# value into SQL DBNull.
        /// </summary>
        private static object DbValue(object value)
        {
            return value ?? DBNull.Value;
        }


        /// <summary>
        /// Safely reads a nullable string from SQL.
        /// </summary>
        private static string GetNullableString(
            SqlDataReader reader,
            string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);

            return reader.IsDBNull(ordinal)
                ? null
                : reader.GetString(ordinal);
        }


        /// <summary>
        /// Safely reads a nullable int from SQL.
        /// </summary>
        private static int? GetNullableInt(
            SqlDataReader reader,
            string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);

            return reader.IsDBNull(ordinal)
                ? (int?)null
                : reader.GetInt32(ordinal);
        }


        /// <summary>
        /// Safely reads a nullable DateTime from SQL.
        /// </summary>
        private static DateTime? GetNullableDateTime(
            SqlDataReader reader,
            string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);

            return reader.IsDBNull(ordinal)
                ? (DateTime?)null
                : reader.GetDateTime(ordinal);
        }


        /// <summary>
        /// Safely reads a nullable boolean from SQL.
        /// </summary>
        private static bool? GetNullableBool(
            SqlDataReader reader,
            string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);

            return reader.IsDBNull(ordinal)
                ? (bool?)null
                : reader.GetBoolean(ordinal);
        }

        #endregion


        #region Create Child

        public void CreateChild(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            // member.id represents the selected parent.
            var parent = GetParentById(member.id);

            using (var connection =
                new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "CreateChild";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@ParentFather",
                    DbValue(parent.ParentFather));

                cmd.Parameters.AddWithValue(
                    "@ParentMother",
                    DbValue(parent.ParentMother));

                cmd.Parameters.AddWithValue(
                    "@Title",
                    DbValue(member.Title));

                cmd.Parameters.AddWithValue(
                    "@Name",
                    DbValue(member.Name));

                cmd.Parameters.AddWithValue(
                    "@Surname",
                    DbValue(member.Surname));

                cmd.Parameters.AddWithValue(
                    "@Telephone",
                    DbValue(member.Telephone));

                cmd.Parameters.AddWithValue(
                    "@Mobile",
                    DbValue(member.Mobile));

                cmd.Parameters.AddWithValue(
                    "@Email",
                    DbValue(member.Email));

                cmd.Parameters.AddWithValue(
                    "@Address",
                    DbValue(member.Address));

                cmd.Parameters.AddWithValue(
                    "@Job",
                    DbValue(member.Job));

                cmd.Parameters.AddWithValue(
                    "@Company",
                    DbValue(member.Company));

                cmd.Parameters.AddWithValue(
                    "@Country",
                    DbValue(member.Country));

                cmd.Parameters.AddWithValue(
                    "@Image",
                    DbValue(member.Image));

                cmd.Parameters.AddWithValue(
                    "@HasChildren",
                    member.HasChildren);

                cmd.Parameters.AddWithValue(
                    "@FamilyCodeID",
                    member.FamilyCode);

                cmd.Parameters.AddWithValue(
                    "@DateOfBirth",
                    DbValue(member.DateOfBirth));

                cmd.Parameters.AddWithValue(
                    "@IsAlive",
                    DbValue(member.IsAlive));

                connection.Open();

                cmd.ExecuteNonQuery();
            }
        }

        #endregion


        #region Get Parent

        private Parent GetParentById(int id)
        {
            var parent = new Parent();

            using (var connection =
                new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "GetParentById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@MemberId",
                    id);

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return parent;
                    }

                    var memberId =
                        reader.GetInt32(
                            reader.GetOrdinal("MemberID"));

                    var title =
                        GetNullableString(
                            reader,
                            "Title");

                    parent.MemberLookUpId = memberId;

                    /*
                     * This retains the behaviour of your
                     * existing application.
                     *
                     * Longer term we should remove this because
                     * parent type should not be inferred from Title.
                     */

                    var isMan = true;

                    if (!string.IsNullOrWhiteSpace(title))
                    {
                        switch (title.Trim().ToLowerInvariant())
                        {
                            case "miss":
                            case "ms":
                            case "mrs":
                                isMan = false;
                                break;
                        }
                    }

                    if (isMan)
                    {
                        parent.ParentFather = memberId;
                        parent.ParentMother = null;
                    }
                    else
                    {
                        parent.ParentFather = null;
                        parent.ParentMother = memberId;
                    }
                }
            }

            return parent;
        }

        #endregion


        #region Save Member

        public void SaveMember(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            using (var connection =
                new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "UpdateMember";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@MemberId",
                    member.id);

                cmd.Parameters.AddWithValue(
                    "@Name",
                    DbValue(member.Name));

                cmd.Parameters.AddWithValue(
                    "@Surname",
                    DbValue(member.Surname));

                cmd.Parameters.AddWithValue(
                    "@Telephone",
                    DbValue(member.Telephone));

                cmd.Parameters.AddWithValue(
                    "@Mobile",
                    DbValue(member.Mobile));

                cmd.Parameters.AddWithValue(
                    "@Email",
                    DbValue(member.Email));

                cmd.Parameters.AddWithValue(
                    "@Address",
                    DbValue(member.Address));

                cmd.Parameters.AddWithValue(
                    "@Image",
                    DbValue(member.Image));

                cmd.Parameters.AddWithValue(
                    "@Job",
                    DbValue(member.Job));

                cmd.Parameters.AddWithValue(
                    "@Company",
                    DbValue(member.Company));

                cmd.Parameters.AddWithValue(
                    "@Country",
                    DbValue(member.Country));

                cmd.Parameters.AddWithValue(
                    "@IsAlive",
                    DbValue(member.IsAlive));

                cmd.Parameters.AddWithValue(
                    "@Title",
                    DbValue(member.Title));

                cmd.Parameters.AddWithValue(
                    "@DateOfBirth",
                    DbValue(member.DateOfBirth));

                connection.Open();

                cmd.ExecuteNonQuery();
            }
        }

        #endregion


        #region Get Tree

        public Members GetAllMembers()
        {
            var rootNodes = new Members();

            using (var connection =
                new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "GetRootMembers";
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var member = new Member
                        {
                            id =
                                reader.GetInt32(
                                    reader.GetOrdinal("MemberId")),

                            Name =
                                GetNullableString(
                                    reader,
                                    "Name"),

                            Title =
                                GetNullableString(
                                    reader,
                                    "Title"),

                            Surname =
                                GetNullableString(
                                    reader,
                                    "Surname"),

                            Telephone =
                                GetNullableString(
                                    reader,
                                    "Telephone"),

                            Email =
                                GetNullableString(
                                    reader,
                                    "Email"),

                            HasChildren =
                                reader.GetBoolean(
                                    reader.GetOrdinal(
                                        "HasChildren"))
                        };

                        rootNodes.Add(member);
                    }
                }
            }


            foreach (var node in rootNodes)
            {
                node.nodes =
                    node.HasChildren
                        ? GetChildren(node)
                        : null;
            }

            return rootNodes;
        }


        private Members GetChildren(Member node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var children =
                GetChildreByParentId(node.id);

            foreach (var child in children)
            {
                child.nodes =
                    child.HasChildren
                        ? GetChildren(child)
                        : null;
            }

            return children;
        }


        public Members GetChildreByParentId(int parentId)
        {
            var members = new Members();

            using (var connection =
                new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "GetChildrenByParentID";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@ParentID",
                    parentId);

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var member = new Member
                        {
                            id =
                                reader.GetInt32(
                                    reader.GetOrdinal(
                                        "MemberId")),

                            Name =
                                GetNullableString(
                                    reader,
                                    "Name"),

                            Surname =
                                GetNullableString(
                                    reader,
                                    "Surname"),

                            Title =
                                GetNullableString(
                                    reader,
                                    "Title"),

                            HasChildren =
                                reader.GetBoolean(
                                    reader.GetOrdinal(
                                        "HasChildren"))
                        };

                        members.Add(member);
                    }
                }
            }

            return members;
        }

        #endregion


        #region Get Member

        public Member GetMemberById(int id)
        {
            Member member = null;

            using (var connection =
                new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "GetMemberById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@UserId",
                    id);

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    member = new Member
                    {
                        id =
                            reader.GetInt32(
                                reader.GetOrdinal(
                                    "MemberId")),

                        Title =
                            GetNullableString(
                                reader,
                                "Title"),

                        Name =
                            GetNullableString(
                                reader,
                                "Name"),

                        Surname =
                            GetNullableString(
                                reader,
                                "Surname"),

                        Telephone =
                            GetNullableString(
                                reader,
                                "Telephone"),

                        Mobile =
                            GetNullableString(
                                reader,
                                "Mobile"),

                        Email =
                            GetNullableString(
                                reader,
                                "Email"),

                        Address =
                            GetNullableString(
                                reader,
                                "Address"),

                        Country =
                            GetNullableString(
                                reader,
                                "Country"),

                        Job =
                            GetNullableString(
                                reader,
                                "Job"),

                        Company =
                            GetNullableString(
                                reader,
                                "Company"),

                        Image =
                            GetNullableString(
                                reader,
                                "Image"),

                        FamilyCode =
                            reader.IsDBNull(
                                reader.GetOrdinal(
                                    "FamilyCodeID"))
                                ? 0
                                : reader.GetInt32(
                                    reader.GetOrdinal(
                                        "FamilyCodeID")),

                        DateOfBirth =
                            GetNullableDateTime(
                                reader,
                                "DOB"),

                        IsAlive =
                            GetNullableBool(
                                reader,
                                "IsAlive"),

                        HasChildren =
                            reader.GetBoolean(
                                reader.GetOrdinal(
                                    "HasChildren"))
                    };
                }
            }

            return member;
        }

        #endregion
    }
}
