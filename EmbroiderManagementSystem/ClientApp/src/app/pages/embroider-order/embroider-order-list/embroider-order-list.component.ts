import { HttpParams } from "@angular/common/http";
import { Component, OnInit, ViewChild } from "@angular/core";
import { Router } from "@angular/router";
import { DxDataGridComponent } from "devextreme-angular";
import CustomStore from "devextreme/data/custom_store";
import { NzMessageService } from "ng-zorro-antd/message";
import { NzModalService } from "ng-zorro-antd/modal";
import { NzNotificationService } from "ng-zorro-antd/notification";
import { AuthService } from "src/app/core/auth.service";
import { EmbroiderOrderService } from "../../../service/embroider-order/embroider-order.service";
import { EmbroiderService } from "../../../service/embroider/embroider.service";
import { ProductService } from "../../../service/product/product.service";

@Component({
  selector: "app-embroider-order-list",
  templateUrl: "./embroider-order-list.component.html",
  styleUrls: ["./embroider-order-list.component.scss"]
})

export class EmbroiderOrderListComponent implements OnInit {
  @ViewChild('clientGrid') clientGrid: DxDataGridComponent;
  dataSource: any = {};
  lookupOrderTypeData = {
    store: {
      type: 'array',
      data: [
        { id: 5, name: 'ဆိုင်ထည်' },
        { id: 10, name: 'အပ်ထည်' },
        { id: 15, name: 'ကျောက်ထည်' },
        // ...
      ],
      key: "id"
    }
  };

  lookupMaterialTypeData = {
    store: {
      type: 'array',
      data: [
        { id: 5, name: 'ရွှေ' },
        { id: 10, name: 'ကျောက်' },
      ],
      key: "id"
    }
  };

  lookupOrderStatusData = {
    store: {
      type: 'array',
      data: [
        { id: 5, name: 'သိမ်းဆည်းထားသည်' },
        { id: 10, name: 'invoice ဖွင့်ထားဆဲ' },
        { id: 15, name: 'အကုန်ပြီးစီး' },
        { id: 20, name: 'ပယ်ဖျက်' },
      ],
      key: "id"
    }
  };
  constructor(private router: Router, private modal: NzModalService, private productService: ProductService, private embroiderService: EmbroiderService, private messageService: NzMessageService, private notificationService: NzNotificationService, private service: EmbroiderOrderService, private authService: AuthService) {
    function isNotEmpty(value: any): boolean {
      return value !== undefined && value !== null && value !== "";
    }

    this.dataSource = new CustomStore({
      key: "id",
      load: function (loadOptions: any) {
        let params: HttpParams = new HttpParams();
        [
          "skip",
          "take",
          "requireTotalCount",
          "requireGroupCount",
          "sort",
          "filter",
          "totalSummary",
          "group",
          "groupSummary"
        ].forEach(function (i) {
          if (i in loadOptions && isNotEmpty(loadOptions[i]))
            params = params.set(i, JSON.stringify(loadOptions[i]));
        });
        return service.getPaginate(params)
          .toPromise()
          .then((data: any) => {
            console.log(data.data);
            return {
              data: data.data,
              totalCount: data.totalCount,
              summary: data.summary,
              groupCount: data.groupCount
            };
          })
          .catch(error => { throw 'Data Loading Error' });
      }
    });
  }
  ngOnInit() {

  }

  createEmbroiderOrder() {
    this.router.navigate([`/dashboard/embroiderorder/embroiderorder`]);
  }
  onToolbarPreparing(e) {
    e.toolbarOptions.items.unshift(
      {
        location: 'after',
        widget: 'dxButton',
        options: {
          icon: 'add',
          onClick: this.createEmbroiderOrder.bind(this)
        }
      });
  }

  edit(data: any) {
    this.router.navigate([`/dashboard/embroiderorder/embroiderorder/${data.id}`]);
  }

  addInv(data: any) {
    this.router.navigate([`/dashboard/embroiderinvoice/newinvoice/${data.id}`]);

  }
}
