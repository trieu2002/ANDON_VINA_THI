import { Modal, Form, Input, Select, Button } from "antd";
import { useState, useEffect } from "react";
import axiosInstance from "../config/axios-customize";
import { toast } from "react-toastify";

const { Option } = Select;

interface AddErrorModalProps {
  visible: boolean;
  onCancel: () => void;
  onSuccess: () => void;
}

interface Route {
  routeId: string;
  code: string;
  routeName: string;
}
interface ErrorName{
   code:string,
   routerName:string,
   errorName:string
}

const AddErrorModal: React.FC<AddErrorModalProps> = ({ visible, onCancel, onSuccess }) => {
  const [form] = Form.useForm();
  const [routes, setRoutes] = useState<Route[]>([]);
  const [errors, setErrors] = useState<ErrorName[]>([]);
  const [selectedRouteId, setSelectedRouteId] = useState<number | null>(null);
  const [isOtherError, setIsOtherError] = useState<boolean>(false);

  const user = localStorage.getItem("user") || "";

  // Lấy danh sách công đoạn khi mở modal
  useEffect(() => {
    if (visible) {
      fetchRoutes();
    }
  }, [visible]);

  const fetchRoutes = async () => {
    try {
      const res = await axiosInstance.get("/router/list");
      console.log('res',res.data?.routes);
      setRoutes(res.data?.routes || []);
    } catch (error) {
      toast.error("Không thể tải danh sách công đoạn");
    }
  };

  // Khi chọn công đoạn → gọi API lấy lỗi
  const handleRouteChange = async (routeId: number) => {
   console.log('id',routeId);
    setSelectedRouteId(routeId);
    setIsOtherError(false);
    form.setFieldsValue({ errorDetail: undefined, customError: undefined });

    try {
      const res = await axiosInstance.get(`/router/${routeId}`);
      setErrors(res.data?.errors || []); // API trả về list lỗi
    } catch (error) {
      toast.error("Không thể tải danh sách lỗi");
      setErrors([]);
    }
  };

  const handleFinish = async (values: any) => {
    console.log("values", values);
    const finalErrorName =
      values.errorDetail === "Khác" ? values.customError : values.errorDetail;

    const payload = {
      lineCode: values.sender,
      routeId: selectedRouteId,
      errorName: finalErrorName,
      errorDescription: values.description || "",
      detectedBy: values.detectedBy || "",
      operator: values.operator || "",
    };

    try {
      const response = await axiosInstance.post("/defect/insert", payload);
      if (response.data.status === 201) {
        toast.success("Thêm mới thành công", { autoClose: 2000 });
        form.resetFields();
        onCancel();
        onSuccess();
      } else {
        toast.error("Thêm mới thất bại", { autoClose: 2000 });
      }
    } catch (error) {
      toast.error("Lỗi khi thêm mới", { autoClose: 2000 });
    }
  };

  return (
    <Modal
      open={visible}
      onCancel={onCancel}
      title={<span style={{ color: "red", fontWeight: "bold" }}>THÊM LỖI</span>}
      width={800}
      footer={null}
      destroyOnClose
    >
      <Form
        form={form}
        layout="vertical"
        initialValues={{ sender: user }}
        onFinish={handleFinish}
      >
        <Form.Item label="Người gửi" rules={[{ required: true }]} name="sender">
          <Input placeholder="Người gửi" disabled />
        </Form.Item>

        <Form.Item
          label="Chọn công đoạn"
          name="errorType"
          rules={[{ required: true }]}
        >
          <Select
            placeholder="Chọn công đoạn"
            onChange={handleRouteChange}
            allowClear
          >
            {routes.map((route) => (
              <Option key={route.routeId} value={route.routeId}>
                {route.routeName}
              </Option>
            ))}
          </Select>
        </Form.Item>

        <Form.Item
          label="Chọn tên lỗi"
          name="errorDetail"
          rules={[{ required: true }]}
        >
          <Select placeholder="Chọn tên lỗi" allowClear>
            {errors.map((err, index) => (
              <Option key={index} value={err.errorName}>
                {err.errorName}
              </Option>
            ))}
            {/* <Option value="Khác">Lỗi khác</Option> */}
          </Select>
        </Form.Item>

        {isOtherError && (
          <Form.Item
            label="Tên lỗi khác"
            name="customError"
            rules={[
              { required: true, message: "Vui lòng nhập tên lỗi khác" },
            ]}
          >
            <Input placeholder="Nhập tên lỗi khác" />
          </Form.Item>
        )}

        <Form.Item
          label="Mô tả lỗi"
          name="description"
          rules={[{ required: true }]}
        >
          <Input placeholder="Nhập mô tả lỗi (nếu có)" />
        </Form.Item>

        <Form.Item
          label="Người phát hiện"
          name="detectedBy"
          rules={[{ required: true }]}
        >
          <Input placeholder="Nhập người phát hiện" />
        </Form.Item>

        <Form.Item
          label="Người thao tác"
          name="operator"
          rules={[{ required: true }]}
        >
          <Input placeholder="Nhập người thao tác" />
        </Form.Item>

        <Form.Item>
          <Button type="primary" htmlType="submit" block>
            Gửi đi
          </Button>
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default AddErrorModal;
