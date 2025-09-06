const ROUTES = [
  {
    code: "ROUTE1",
    name: "WINDING-권취_CUỐN",
    errors: [
      "치수불량(권취)_Winding_ NG kích thước_NG Size",
      "쇼트불량Winding_ NG short",
      "박장 길이 불량 NG độ dài điên cực",
      "집전체 찢어짐 불량 Rách điện cực",
      "T 치수 불량 NG kích thước T",
      "F 치수 불량 NG kích thước F",
      "전극 돌출 불량 Lỗi hở điện cực",
      "분리막 찢어짐 불량 Lỗi rách giấy ngăn",
      "단자 휨 불랑 Cong chân tancha"
    ]
  },
  {
    code: "ROUTE2",
    name: "BEADING-비딩_BO CỔ",
    errors: [
      "소자낙하(고무전조립)_VA CHẠM SOCHA (CHÈN CAO SU)",
      "고무전조립_고무전손상Lắp cao su_ cao su hư hỏng",
      "내전압불량Curling_ Lỗi điện áp trong"
    ]
  },
  {
    code: "ROUTE3",
    name: "CURLING-커링_CUỐN ĐỈNH",
    errors: [
      "소자낙하(함침)_VA CHẠM SOCHA (NGÂM DUNG DỊCH)",
      "비딩불량_LỖI BEADING",
      "Case찌그러짐_MÉO, BẸP CASE",
      "내전압불량Curling_ Lỗi điện áp trong",
      "ESR불량(특성검사)_LỖI ESR (KIỂM TRA ĐẶC TÍNH)",
      "커링형상불량(외관검사)Ngoại quan_ Lỗi biên dạng curling",
      "봉누액(외관검사)_tràn dịch lỗ cao su",
      "리드꼬임불량_ Lỗi lộ chân tancha",
      "누액(외관검사)_Ngoại quan_Tràn dịch",
      "커링 치수 불량_NG kích thước curling"
    ]
  },
  {
    code: "ROUTE4",
    name: "SLEEVE-슬리브_BỌC VỎ",
    errors: ["Issue 1", "Issue 2", "Issue 3", "Issue 4"]
  },
  {
    code: "ROUTE5",
    name: "AGING-에이징_SẠC XẢ",
    errors: ["용량불량 NG dung lượng", "AC-ESR 불량 NG ESR", "S/D 불량 NG S/D"]
  },
  {
    code: "ROUTE6",
    name: "VISUAL-외관_NGOẠI QUAN",
    errors: [
      "리드Scratch(외관검사)Ngoại quan_ xước chân tancha",
      "리드돌출(외관검사)Ngoại quan_ Lỗi lộ chân tancha",
      "커링형상불량(외관검사)Ngoại quan_ Lỗi biên dạng curling",
      "고무전불량(외관검사)Ngoại quan_ Lỗi cao su",
      "Case찌그러짐(외관검사)Ngoại quan_Bẹp vỏ nhôm",
      "세척불량(외관검사)Ngoại quan_ Lỗi rửa",
      "리드변색(외관검사)Ngoại quan_ Biến sắc chân tancha",
      "슬리브불량Ngoại quan_ NG vỏ",
      "리드함몰(외관검사)Ngoại quan_Thụt chân tancha",
      "누액(외관검사)Ngoại quan_Tràn dịch",
      "봉누액(외관검사)Ngoại quan_tràn dịch cao su",
      "슬리브 역삽입(외관검사)Ngoại quan_Lắp ngược"
    ]
  },
  {
    code: "ROUTE7",
    name: "PACKING-포장_ĐÓNG GÓI",
    errors: ["Issue 1", "Issue 2", "Issue 3", "Issue 4"]
  },
  {
    code: "ROUTE8",
    name: "TAPING-테이핑_DÁN BĂNG DÍNH",
    errors: [
      "단지 휨 Cong chân tancha",
      "H(높이) 치수 불량 Lỗi kích thước H (Độ cao)",
      "단지 스크래치 불량 Lỗi xước tancha"
    ]
  },
  {
    code: "ROUTE9",
    name: "BENDING-벤딩_BẺ CHÂN",
    errors: ["Issue 1", "Issue 2", "Issue 3", "Issue 4"]
  },
  {
    code: "ROUTE10",
    name: "PQC_KIỂM TRA CÔNG ĐOẠN",
    errors: ["Issue 1", "Issue 2", "Issue 3", "Issue 4"]
  },
  {
    code: "ROUTE11",
    name: "OQC_KIỂM TRA THÀNH PHẨM",
    errors: ["Issue 1", "Issue 2", "Issue 3", "Issue 4"]
  },
  {
    code: "ROUTE12",
    name: "FOQC_KIỂM TRA FOQC",
    errors: ["Issue 1", "Issue 2", "Issue 3", "Issue 4"]
  },
  {
    code: "ROUTE13",
    name: "TQC_KIỂM TRA TQC",
    errors: ["lỗi 1", "lỗi 2", "lỗi 3", "lỗi 4"]
  },
  {
    code: "ROUTE14",
    name: "MODULE_HÀNG MÔ ĐUN",
    errors: ["Lỗi 1"]
  },
  {
    code: "ROUTE15",
    name: "MIXING_TRỘN ĐIỆN CỰC",
    errors: ["Lỗi 1"]
  },
  {
    code: "ROUTE16",
    name: "COATING_MẠ ĐIỆN CỰC",
    errors: ["Lỗi 1"]
  },
  {
    code: "ROUTE17",
    name: "ROLLPRESS_ÉP ĐIỆN CỰC",
    errors: ["Lỗi 1"]
  },
  {
    code: "ROUTE18",
    name: "SLITTING_CẮT ĐIỆN CỰC",
    errors: ["Lỗi 1"]
  }
];
export default ROUTES;