import axios from "axios";
const axiosInstance=axios.create({
    baseURL:"https://localhost:44348/api/v1/",
    withCredentials:true,
    headers:{
        "Content-Type": "application/json; charset=utf-8"
    }
});
axiosInstance.interceptors.request.use(
  (config) => {
    if (typeof window !== "undefined") {
      const token = localStorage.getItem("access_token");
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
    }
    return config;
  },
  (error) => Promise.reject(error)
);

axiosInstance.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.statusCode === 401) {
      console.error("Token hết hạn hoặc không hợp lệ, vui lòng đăng nhập lại!");
      localStorage.removeItem("access_token"); // Xóa token nếu lỗi 401
      
      // Kiểm tra nếu đang chạy trên trình duyệt mới nếu mà không đăng nhập chính xác sẽ chuyển về trang đăng nhập
      if (typeof window !== "undefined") {
        window.location.href = "/login"; // Chuyển hướng về trang đăng nhập
      }
    }
    return Promise.reject(error);
  }
);

export default axiosInstance;

