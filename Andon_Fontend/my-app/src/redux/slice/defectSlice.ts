import axiosInstance from "../../config/axios-customize";
import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import type { IDefectList } from "../../types/backend";
interface DefectState {
  data: IDefectList[];
  status: "pending" | "loading" | "succeeded" | "failed";
  error: string | null;
};
const initialState: DefectState = {
  data: [],
  status: "pending",
  error: null,
};
export const fetchDefetList = createAsyncThunk(
  "defectList",
  async (
   {
      fromDate,
      toDate,
    }: { fromDate?: string; toDate?: string},
    {  }
  ) => {
    try {
      const response = await axiosInstance.get("/defect/list", {
        params: { fromDate, toDate },
      });
      return response?.data?.defect;
    } catch (error: any) {
      console.log('err',error);
    }

  }
);
const defectSlice=createSlice({
    name:"defect",
    initialState,
    reducers:{
        
    },
    extraReducers: (builder) => {
         builder
      .addCase(fetchDefetList.pending, (state) => {
        state.status = "loading";
      })
      .addCase(fetchDefetList.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.data = action.payload || [];
      })
      .addCase(fetchDefetList.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.payload as string;
      });
    }

});
export default defectSlice.reducer;
