using blogapi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace blog.Controllers
{
    [ApiController]
    public class blog : ControllerBase
    {

        string conqry = @"Data Source=VISHAL\SQLEXPRESS;Initial Catalog=blog;Integrated Security=True;Connect Timeout=3600;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        [HttpPost("api/blog/add")]
        public dynamic AddSubject(blogreq req)
        {
            using (SqlConnection con = new SqlConnection(conqry))
            {
                string qry = "INSERT INTO blog(sid,writer,title,synopsis,description,corporate_action,languages) VALUES('" + req.sid + "','" + req.writer + "','" + req.title + "','" + req.synopsis + "','" + req.description + "','" + req.corporate_action + "','" + req.languages + "')";
                using (SqlCommand cmd = new SqlCommand(qry, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Ok("insert Successfully");
        }

        [HttpPost("api/blog/update")]
        public dynamic UpdateBlog(blogreq req)
        {
            using (SqlConnection con = new SqlConnection(conqry))
            {
                string qry = "UPDATE blog SET sid='" + req.sid + "', writer='" + req.writer + "',title='" + req.title + "',synopsis='" + req.synopsis + "',description='" + req.description + "',corporate_action='" + req.corporate_action + "',languages='" + req.languages + "' where bid='" + req.bid + "'";

                using (SqlCommand cmd = new SqlCommand(qry, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            return ("update Successfully");
        }

        [HttpDelete("api/blog/delete")]
        public dynamic DeleteBlog([FromQuery] int bid)
        {
            using (SqlConnection con = new SqlConnection(conqry))
            {
                string qry = "DELETE FROM blog where bid=" + bid;
                using (SqlCommand command = new SqlCommand(qry, con))
                {
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Ok("Deleted...");

        }

        [HttpGet("api/blog/list")]
        public dynamic GetBlogList()
        {
            DataTable dtDepart = Getblog();
            List<blogreq> blogList = new List<blogreq>();
            foreach (DataRow drDepart in dtDepart.Rows)
            {
                blogList.Add(new blogreq()
                {   
                    bid = System.Convert.ToInt32(drDepart["bid"]),
                    sid = System.Convert.ToInt32(drDepart["sid"]),
                    writer = drDepart["writer"].ToString(),
                    title = drDepart["title"].ToString(),
                    synopsis = drDepart["synopsis"].ToString(),
                    description = drDepart["description"].ToString(),
                    corporate_action = drDepart["corporate_action"].ToString(),
                    languages = drDepart["languages"].ToString(),

                });
            }
            return Ok(blogList);
        }
        private DataTable Getblog()
        {
            DataTable dtDepart = new DataTable();
            using (SqlConnection con = new SqlConnection(conqry))
            {
                string qry = "select * from blog";
                using (SqlCommand command = new SqlCommand(qry, con))
                {
                    SqlDataAdapter adaptor = new SqlDataAdapter(command);
                    con.Open();
                    adaptor.Fill(dtDepart);
                    con.Close();
                }
            }
            return dtDepart;
        }

        [HttpGet("api/blog/detail")]
        public async Task<blogreq> Getblogdetail([FromQuery] int bid)
        {
            var blog = new blogreq();
            using (SqlConnection con = new SqlConnection(conqry))
            {
                con.Open();
                string sql = "Select * from blog where bid=" + bid;
                using (SqlCommand comm = new SqlCommand(sql, con))
                {
                    comm.Parameters.AddWithValue("bid", bid);
                    var reader = await comm.ExecuteReaderAsync();
                    int id = reader.GetOrdinal("bid");
                    int sid = reader.GetOrdinal("sid");
                    int writer = reader.GetOrdinal("writer");
                    int title = reader.GetOrdinal("title");
                    int synopsis = reader.GetOrdinal("synopsis");
                    int description = reader.GetOrdinal("description");
                    int corporate_action = reader.GetOrdinal("corporate_action");
                    int languages = reader.GetOrdinal("languages");
                    while (reader.Read())
                    {
                        blog.bid = reader.GetInt32(id);
                        blog.sid = reader.GetInt32(sid);
                        blog.writer = reader.GetString(writer);
                        blog.title = reader.GetString(title);
                        blog.synopsis = reader.GetString(synopsis);
                        blog.description = reader.GetString(description);
                        blog.corporate_action = reader.GetString(corporate_action);
                        blog.languages = reader.GetString(languages);
                        
                    }

                    return (blog);
                }
            }

        }

    }
}
