import React from 'react';
import { Modal, Form, Input, Button } from 'antd';
import axiosInstance from '../config/axios-customize';
import { toast } from 'react-toastify';

interface CompleteRepairModalProps {
  visible: boolean;
  onCancel: () => void;
  id:number,
  onSuccess:()=> void
}

const CompleteRepairModal: React.FC<CompleteRepairModalProps> = ({
  visible,
  onCancel,
  id,
  onSuccess
}) => {
  const [form] = Form.useForm();

  const handleFinish =async (values: any) => {
      console.log('người gửi',values);
      // call api 
      const response=await axiosInstance.put("/defect/complete",{
        id:id,
        repairer:values.repairer,
        reason:values.reason,
        countermeasure:values.countermeasure
      });
      if(response.data.status===201){
         toast.success("Cập nhật sửa lỗi thành công",{autoClose:2000});
          form.resetFields();  
          onCancel();
          onSuccess();
      }else{
          toast.error("Cập nhật sửa lỗi thất bại",{autoClose:2000});
      }
  };

  return (
    <Modal
      title="Cập nhật :"
      open={visible}
      onCancel={() => {
        form.resetFields();
        onCancel();
      }}
      footer={null}
      width={700}
      centered
    >
      <Form form={form} layout="vertical" onFinish={handleFinish}>
        <Form.Item label="Người gửi" rules={[{ required: true, message: 'Vui lòng nhập người gửi' }]} name="user">
          <Input placeholder='Người gửi'  />
        </Form.Item>

        <Form.Item
          name="reason"
          label="Nguyên nhân"
          rules={[{ required: true, message: 'Vui lòng nhập nguyên nhân' }]}
        >
          <Input placeholder="Nhập nguyên nhân" />
        </Form.Item>

        <Form.Item
          name="countermeasure"
          label="Đối sách"
          rules={[{ required: true, message: 'Vui lòng nhập đối sách' }]}
        >
          <Input placeholder="Nhập đối sách" />
        </Form.Item>
        <Form.Item
          name="repairer"
          label="Người sửa"
          rules={[{ required: true, message: 'Vui lòng nhập người sửa' }]}
        >
          <Input placeholder="Nhập tên người sửa" />
        </Form.Item>
        <Form.Item>
          <Button type="primary" htmlType="submit">
            Gửi đi
          </Button>
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default CompleteRepairModal;
