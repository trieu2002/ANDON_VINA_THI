import { useEffect, useState } from 'react';
import { Table, Select, Button, Space, Row, Col, Tag } from 'antd';
import {  PlusOutlined, FileExcelOutlined, LogoutOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import type { AppDispatch, RootState } from '../redux/store/store';
import { fetchDefetList } from '../redux/slice/defectSlice';
import AddErrorModal from './AddErrorModal'; 
import axiosInstance from '../config/axios-customize';
import { toast } from 'react-toastify';
import CompleteRepairModal from './CompleteRepairModal';
const { Option } = Select;


const AndonPage = () => {
  const { data } = useSelector((state: RootState) => state.defect);
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isCompleteOpen,setIsCompleteOpen]=useState<boolean>(false);
  const [statusFilter, setStatusFilter] = useState<string | null>(null);
  const [id,setId]=useState<number>(0);
  const dispatch = useDispatch<AppDispatch>();
  const navigate=useNavigate();
  useEffect(() => { 
     const interval = setInterval(() => {
        dispatch(fetchDefetList({}));
     }, 3000); // 3 giây 1 lần
      return () => clearInterval(interval);
   }, []);

  const handleStartRepair =async (id:number) => {
    console.log('Start Repair:',typeof id);
    try {
       const res = await axiosInstance.put(`/defect/update/${id}`);
       console.log('ress',res.data.status);

    if (res.data.status === 201) {
       toast.success("Bắt đầu sửa lỗi thành công", { autoClose: 2000 });
       dispatch(fetchDefetList({}));
    }
  } catch (error: any) {
      console.error('Lỗi gọi API:', error);
      toast.error(`Lỗi: ${error.response?.data?.message || 'Không xác định'}`);
  }
  };

  const handleCompleteRepair = (id:any) => {
    setIsCompleteOpen(true);
    setId(id);
  };
  const handleOpenError=()=>{
    setIsModalOpen(true);
  }
  const handleStatusChange=(value:string|null)=>{
    setStatusFilter(value);
  }
  const handleExportExcel=async ()=>{
     try{
         const response=await axiosInstance.get(`/defect/export-excel`,{
            responseType: 'blob' 
         });
          const url = window.URL.createObjectURL(new Blob([response.data]));
          const link = document.createElement('a');
          link.href = url;
          link.setAttribute('download', 'Defect_Export.xlsx');
          document.body.appendChild(link);
          link.click();
          link.remove();
     }catch(error){
         console.error('Lỗi export:', error);
     }
  }
  return (
   <div className="p-4" style={{width:'100%'}}>
      <Row justify="space-between" align="middle" style={{margin:"20px 20px 20px 20px"}}>
        <Col>
           <Select
            placeholder="Chọn trạng thái"
            onChange={handleStatusChange}
            style={{ width: 500 }}
            value={statusFilter}
            allowClear
          >
            <Option value={null}>Tất cả</Option>
            <Option value="1">Đã hoàn thành</Option>
            <Option value="0">Chưa hoàn thành</Option>
          </Select>
        </Col>

        <Col>
          <Space>
           <Button type="primary" icon={<FileExcelOutlined />} style={{ backgroundColor: '#52c41a', borderColor: '#52c41a' }} onClick={handleExportExcel} >
              Xuất Excel
            </Button>
            <Button type="primary" icon={<PlusOutlined />} onClick={handleOpenError} style={{cursor:"poiter"}}>Thêm Lỗi</Button>
             <Button
                onClick={() => {
                localStorage.removeItem('access_token'); 
                localStorage.removeItem("user"); 
                localStorage.removeItem("groupId"); 
                navigate('/login'); 
             }}
              danger
               icon={<LogoutOutlined />}
               >
              Đăng xuất
             </Button>
          </Space>
        </Col>
      </Row>

      <Table bordered={true} style={{fontSize:"18px"}}  scroll={{ x: true }} columns={[
        {
          title: 'Mã_Line',
          dataIndex: 'lineCode',
          key: 'lineCode',
          width: 50,
          align: 'center',
        },
        
        {
          title: 'Công_đoạn',
          dataIndex: 'routeName',
          key: 'routeName',
          width: 200,
        },
        {
          title: 'Tên_lỗi',
          dataIndex: 'errorName',
          key: 'errorName',
          width: 400,
        },
        {
          title: 'Mô_tả_chi_tiết_lỗi',
          dataIndex: 'errorDescription',
          key: 'errorDescription',
          width: 150,
          align: 'center',
        },
        {
          title: 'Người_phát_hiện',
          dataIndex: 'detectedBy',
          key: 'detectedBy',
          width: 100,
          align: 'center',
        },
        {
          title: 'Người_thao_tác',
          dataIndex: 'operator',
          key: 'operator',
          width: 100,
          align: 'center',
        },
        {
          title: 'Nguyên_nhân',
          dataIndex: 'reason',
          key: 'reason',
          width: 150,
        },
        {
          title: 'Đối_sách',
          dataIndex: 'countermeasure',
          key: 'countermeasure',
          width: 150,
          align: 'center',
        },
        {
          title: 'Người_sửa',
          dataIndex: 'repairer',
          key: 'repairer',
          width: 100,
          align: 'center',

        },
          {
        title: 'Bắt_đầu_sửa',
        dataIndex: 'beginFix',
        key: 'beginFix',
        width: 100,
        align: 'center',
        render: ( _, record) => {
        return !record.beginFix ? (
        <Button type="primary" danger onClick={() => handleStartRepair(record.id)}>
            Bắt đầu sửa
        </Button>
    ) : (
        <span>{new Date(record.beginFix).toLocaleString()}</span> 
    );
    }
     },
        {
          title: 'Trạng_thái',
          dataIndex: 'status',
          key: 'status',
          width: 100,
          align: 'center',
         render: (_, record) => {
           return record.status === 0 ? (
         <Tag color="red">Chưa hoàn thành</Tag>
          ) : (
         <Tag color="green">Hoàn thành</Tag>
    );
     }
    },
    {
          title: 'Kết_thúc_sửa',
          dataIndex: 'finishFix',
          key: 'finishFix',
          width: 100,
          align: 'center',
           render: (_, record) => {
            if (!record.beginFix) {
              return (
                <Button
                  type="primary"
                  danger
                  style={{ backgroundColor: '#52c41a', borderColor: '#52c41a' }}
                  onClick={() => {
                  toast.warning("Bạn cần bắt đầu sửa trước khi hoàn thành!", {
                  autoClose: 2000
                });
          }}
              >
               Hoàn thành
               </Button>
         );
       }
        return !record.finishFix ? (
        <Button type="primary" danger onClick={() => handleCompleteRepair(record.id)} style={{ backgroundColor: '#52c41a', borderColor: '#52c41a' }}>
            Hoàn thành
        </Button>
          ) : (
          <span>{new Date(record.finishFix).toLocaleString()}</span> 
      );
       }
      },
        {
          title: 'Thời_gian_sửa',
          dataIndex: 'repairDuration',
          key: 'repairDuration',
          width: 100,
          align: 'center',
           render: (_, record) => {
   
             return `${record.repairDuration} phút`;
        }
        },
        {
          title: 'Thời_gian_tạo',
          dataIndex: 'createdAt',
          key: 'createdAt',
          align: 'center',
          width: 100,
          render: (_,record) => {
            return <span>{new Date(record.createdAt).toLocaleString()}</span>;
          }
      }
      ]} dataSource={
         statusFilter === null
         ? data
         : data.filter(item => String(item.status) === statusFilter)
         } pagination={false}   />
        {isModalOpen===true && <AddErrorModal
          visible={isModalOpen}
          onCancel={() => setIsModalOpen(false)}
          onSuccess={() => {
          dispatch(fetchDefetList({}));
          setIsModalOpen(false);
        }}
      />}
     {isCompleteOpen === true && (
     <CompleteRepairModal 
          visible={isCompleteOpen}
          onCancel={() => setIsCompleteOpen(false)}
          id={id}
          onSuccess={() => {
          dispatch(fetchDefetList({}));
          setIsCompleteOpen(false); 
        }}
  />
)}
    </div>
  );
};

export default AndonPage;
