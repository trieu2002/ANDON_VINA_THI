import LoginImage from '../assets/login-bg-1.png';
import { Form, Input, Button, Card } from "antd";
import'../styles/login.css';
import axiosInstance from '../config/axios-customize';
import { toast } from "react-toastify";
import { useNavigate } from 'react-router-dom';

const Login=()=>{
    const navigate=useNavigate();
    const onFinish=async (values: { username: string; password: string })=>{
         try {
             const response=await axiosInstance.post("/auth/login",{
                username:values.username,
                password:values.password
             });
             console.log("res",response?.data);
            if(response?.data?.status===200){
             console.log('chạy vào day');
                toast.success("Đăng nhập thành công",{autoClose:2000});
                localStorage.setItem("access_token", response.data.user.token);
                localStorage.setItem("user",response.data.user.userName);
                localStorage.setItem("groupId",response.data.user.groupId);
                navigate("/andon");
            }else{
                toast.error("Invalid server response", { autoClose: 3000 });
            }
         } catch (error:any) {
             const errorMessage = error?.response?.data?.UserMessage || "Đăng nhập thất bại";
             toast.error(errorMessage, { autoClose: 3000 });
         }
    }
    return <>
    
       <div className="login-container">
          <div className="login-image">
              <img src={LoginImage} alt='Image Login' />
          </div>
          <Card title="ĐĂNG NHẬP" className='login-card'>
             <Form layout='vertical' onFinish={onFinish}>
                <Form.Item name="username" rules={[{ required: true, message:'Không được để trống'}]} label="Username">
                    <Input placeholder='Nhập username'/>
                </Form.Item>
                <Form.Item name="password" label="Password" rules={[{ required: true, message:'Không được để trống'}]}>
                    <Input.Password placeholder='Nhập password' />
                </Form.Item>
                <Form.Item>
                    <Button type='primary' htmlType='submit' block>
                        Đăng nhập
                    </Button>
                </Form.Item>
             </Form>
          </Card>
          
       </div>
     
    </>
}
export default Login;