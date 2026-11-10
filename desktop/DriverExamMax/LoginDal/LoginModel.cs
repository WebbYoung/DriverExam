namespace DriverExam.LoginDal
{
    public class LoginModel
    {
        private static string ?userName;  //用户名
        private static string ?userPwd;   //密码
        private static int correctNum;   //答对题数
        private static int currentNum;   //已答题数      
		public static string UserName { get => userName??throw new ArgumentNullException(nameof(userName)); set => userName = value; }
        public static string UserPwd { get => userPwd??throw new ArgumentNullException(nameof(userPwd)); set => userPwd = value; }
        public static int CorrectNum { get => correctNum; set => correctNum = value; }
        public static int CurrentNum { get => CurrentNum; set =>currentNum = value; }
	}
}
