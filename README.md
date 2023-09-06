# Blazor_Auth

## 목차

1. [개요](#개요)
2. [Routing 별 레이아웃 설정](#routing-별-layout-설정)
3. [토큰 인증 구조](#토큰-인증-구조)
4. [Server : 토큰 발급](#server--토큰-발급)
   1. [JwtBearer 설치](#jwtbearer-설치)
   2. [JWT 발급](#jwt-발급)
5. [Server : 토큰 검증](#server--토큰-검증)
   1. [JwtBearer 옵션 설정](#jwtbearer-옵션-설정)
   2. [JWT에서 Claim 추출](#jwt에서-claim-추출)
   3. [커스텀 핸들러](#커스텀-핸들러)
6. [Server : 인증 적용](#server--인증-적용)
   1. [Authorize Attribute](#authorize-attribute)
7. [Client : 토큰 관리](#client--토큰-관리)
   1. [세션 스토리지](#세션-스토리지)
   2. [헤더에 토큰 추가](#헤더에-토큰-추가)
   3. [헤더에서 재발급 토큰 로드](#헤더에서-재발급-토큰-로드)
8. [예시 프로젝트 상세설명](#예시-프로젝트-상세설명)

---

## 개요

이 프로젝트는 [Blazor_Study](https://github.com/sueshinkr/Blazor_Study) 레포지터리의 내용을 기반으로 로그인 및 세션 관리 기능을 추가한 것이다.
<br>변경사항은 다음과 같다.

### 1. 로그인 UI

![](images/Blazor_Auth/001.png)

Base Url에 접근시 최초에는 로그인 화면만 나타난다.<br>
로그인하지 않으면 다른 메뉴들을 볼 수 없고, 주소창에 직접 URL을 입력해도 이동되지 않는다.

### 2. 세션 만료시 접근 불가

![](images/Blazor_Auth/003.png)

이 프로젝트에서는 Session Storage에 JWT 기반 Access Token, Refresh Token을 저장하고,<br>
이것으로 Managing API를 호출한다.

Session Storage에 저장된 토큰이 만료되거나 오염된 경우 토큰이 삭제되고 재로그인을 유도한다.

### 3. 경로 별 Layout 설정

- 기존 메뉴
  ![](images/2023-07-24-10-09-36.png)

- 변경 후 메뉴
  ![](images/Blazor_Auth/002.png)

경로 별 Layout을 다르게 적용했고, 메뉴 바(네비게이션 바)가 상단에 수평 형태로 위치하도록 변경했다.

<br>

---

## Routing 별 Layout 설정

| 메인 화면                       | 로그인 이후 화면                |
| ------------------------------- | ------------------------------- |
| ![](images/Blazor_Auth/004.PNG) | ![](images/Blazor_Auth/002.png) |

![](images/Blazor_Auth/005.PNG)

이 프로젝트에서는 라우팅에 따라 다른 **Layout**을 사용하고 있다.<br>
(사용하는 레이아웃은 각각 **MainLayout**, **AfterLoginLayout** 이다.)

### App.razor 라우팅 설정

Blazor에서 라우팅은 **App.razor** 파일에서 설정 가능하다.<br>
App.razor에서 NavigationManager를 주입받고, 경로에 따라 `@if-else` 문을 사용해 레이아웃을 세팅해준다.<br/>
이 프로젝트는 초기 페이지(`NavigationManager.BaseUri`)를 제외하고는 모두 AfterLoginLayout을 적용시켰다.

### 예시 코드

```csharp
@inject NavigationManager NavigationManager

<Router AppAssembly="@typeof(App).Assembly">
    <Found Context="routeData">
        // 현재 경로가 MainLayoutPath라면 MainLayout 사용
        @if (NavigationManager.Uri.Equals(MainLayoutPath))
        {
            <RouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)" />
        }
        else // 그 외엔 AnotherLayout 사용
        {
            <RouteView RouteData="@routeData" DefaultLayout="@typeof(AnotherLayout)" />
        }
        <FocusOnNavigate RouteData="@routeData" Selector="h1" />
    </Found>
    <NotFound>
        // 라우팅 실패...
    </NotFound>
</Router>
```

<br>

---

## 토큰 인증 구조

### 최초 로그인

```mermaid
sequenceDiagram

Blazor Client-)Managing Server: 로그인 요청
Managing Server-)Game API Server: 로그인 요청
Game API Server-)DB: 유저 정보 요청
DB--)Game API Server: 유저 정보 로드

alt 로그인 성공
Note over Game API Server : 토큰 발급
Note over Game API Server : Response 헤더에 토큰 추가
Game API Server-)DB: Refresh Token 저장
end

Game API Server--)Managing Server: 로그인 응답

Managing Server--)Blazor Client: 로그인 응답
```

최초 로그인 성공 시 Access Token, Refresh Token을 발급해 응답 헤더에 추가해 전송한다.

### 토큰 발급 이후 API 호출

```mermaid
sequenceDiagram
Blazor Client-)Managing Server: API 호출
Managing Server-)Game API Server: API 호출
alt AccessToken 만료, RefreshToken 유효
Game API Server--)Managing Server: AccessToken 재발급,<br>API 응답 전송
Managing Server--)Blazor Client: API 응답 전송
Note over Blazor Client : 새 AccessToken을<br>Session Storage에 저장
end
alt AccessToken 만료, RefreshToken 만료
Game API Server--)Managing Server: 401 Unauthorized
Managing Server--)Blazor Client: 401 Unauthorized
Note over Blazor Client : Session Storage 토큰 무효화<br>재로그인 유도
end
```

Managing API 호출 시 토큰을 사용한다.<br>
**(클라이언트는 항상 헤더에 두 토큰을 모두 추가해서 전송한다.)**

Access Token이 만료되어도 Refresh Token이 유효하다면 새 Access Token을 발급해준다.<br>
Refresh Token도 만료되었다면 다시 로그인을 시도해야 한다.

### AccessToken 재발급 로직

1. Refresh Token을 가져와 기한이 만료되었는지 확인한다.
2. Refresh Token의 Claim에서 `AccountId`를 가져온다.
3. DB에서 `AccountId`로 유저를 찾고, **DB의 `RefreshToken`이 헤더의 Refresh Token과 같은지** 비교한다.
4. 같다면 새 Access Token을 발급해 응답 헤더에 추가하고, API 요청을 이어서 수행한다.

<br>

---

# Server : 토큰 발급

## JwtBearer 설치

### 설치

|                                 |                                 |
| ------------------------------- | ------------------------------- |
| ![](images/Blazor_Auth/010.png) | ![](images/Blazor_Auth/011.png) |

Blazor Server, API Server 프로젝트에 Nuget 패키지 관리자 -> 솔루션용 NuGet 패키지 관리에서 `Microsoft.AspNetCore.Authentication.JwtBearer` 를 설치한다.

<br>

---

## JWT 발급

JWT은 헤더(Header), 페이로드(Payload), 서명(Signature)의 3가지 파트로 나눠져 있다.<br>
![](https://velog.velcdn.com/images%2Fhahan%2Fpost%2Fb41e147b-69d0-41ad-9f23-5e1ab8ec35ce%2Fimage.png)

- **헤더** : 어떤 알고리즘을 사용해 암호화 되었는지, 어떤 토큰을 사용하는지에 대한 정보
- **페이로드** : 전달하려는 정보 (단, 노출될 수 있음)
- **서명** : 검증을 위해 서버가 지정한 Signing Key

여기서, Payload에 담는 정보를 클레임(`Claim`)이라고 한다.

토큰은 `JwtSecurityTokenHandler`와 `SecurityTokenDescriptor`를 통해 발급할 수 있다.<br>

- `JwtSecurityTokenHandler` : 토큰 생성 및 검증 클래스
- `SecurityTokenDescriptor` : 토큰에 들어갈 정보를 담은 구조체

### 예시 코드

```csharp
// JwtSecurityTokenHandler 생성
var tokenHandler = new JwtSecurityTokenHandler();

// Signing Key, 토큰 만료 기간 설정
var key = Encoding.ASCII.GetBytes(_signingKey);
var expires = DateTime.UtcNow.AddHours(6);

// 토큰 구조체 생성
var tokenDescriptor = new SecurityTokenDescriptor
{
    Subject = new ClaimsIdentity(new Claim[]
    {
        new Claim("Key", Value),
        // ... 그 외 클레임 추가
    }),
    Expires = expires,
    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
};

// 토큰 생성
var token = tokenHandler.CreateToken(tokenDescriptor);
tokenHandler.WriteToken(token);
```

<br>

---

# Server : 토큰 검증

## JwtBearer 옵션 설정

```csharp
// Program.cs
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,                                       // Issuer 검증 여부
                ValidateAudience = false,                                     // Audience 검증 여부
                ValidateIssuerSigningKey = true,                              // 비밀 서명 키 검증 여부
                ValidateLifetime = true,                                      // 토큰 유효성 검증 여부
                IssuerSigningKey = new SymmetricSecurityKey
                                       (Encoding.UTF8.GetBytes("SigningKey")) // 비밀 서명 키
            };
        });
```

Program.cs에서, `AddAuthentication`으로 `JwtBearer`를 추가하고 토큰 검증에 필요한 옵션들을 작성한다.<br>
위 코드로는 Signing Key와 만료 기간에 대해서만 검증을 수행한다.

옵션은 아래와 같은 것들이 있으며, 더 많은 옵션은 참고 문서에서 확인할 수 있다.

| 옵션                     | 설명                                       |
| ------------------------ | ------------------------------------------ |
| ValidateIssuer           | Issuer(토큰 발행자)에 대한 검증 여부       |
| ValidateAudience         | Audience(토큰 대상자)에 대한 검증 여부     |
| ValidateIssuerSigningKey | Signing Key(비밀 서명 키)에 대한 검증 여부 |
| ValidateLifetime         | 만료 기간에 대한 검증 여부                 |
| IssuerSigningKey         | 비밀 서명키 문자열                         |

[참고 문서 : MSDN TokenValidationParameters](https://learn.microsoft.com/en-us/dotnet/api/microsoft.identitymodel.tokens.tokenvalidationparameters?view=msal-web-dotnet-latest)

<br>

---

## JWT에서 Claim 추출

`JwtSecurityTokenHandler`를 사용해 토큰에서 Claim을 추출한다.<br>
토큰의 Claim은 노출될 위험이 있으므로 이에 주의하여 최소한의 정보를 담아야 한다.

```csharp
var handler = new JwtSecurityTokenHandler();
var readToken = handler.ReadJwtToken(token);

// Claim 추출
var claim = refreshToken.Claims.FirstOrDefault(claim => claim.Type.Equals("Key"));
return claim.Value;
```

### 예시 코드

토큰 페이로드에 `AccountId`라는 Key를 갖는 Claim을 추가했다면, 다음과 같이 사용할 수 있다.

`var accountIdClaim = refreshToken.Claims.FirstOrDefault(claim => claim.Type.Equals("AccountId"));`

<br>

---

## 커스텀 핸들러

### JwtBearerEvents

JwtBearer의 `options.Events`에서 인증 성공/실패 상황에 따라 동작을 정의할 수 있다.<br>
람다식 형태로 사용한다.

```csharp
// Program.cs
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            // JwtBearer를 사용한 Authentication 이벤트에 대한 핸들러 정의
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context => {
                    // 검증 성공 시의 동작
                },

                OnAuthenticationFailed = context =>
                {
                    // 검증 실패 시의 동작
                }
            };
        });
```

[참고 문서 : MSDN JwtBearerEvents](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authentication.jwtbearer.jwtbearerevents?view=aspnetcore-7.0)

<br>

---

# Server : 인증 적용

## Authorize Attribute

```csharp
// Program.cs
app.UseAuthentication();
app.UseAuthorization();
```

Program.cs에서 `UseAuthentication()`, `UseAuthorization()` 호출 후<br>
인증이 필요한 **엔드포인트에 `[Authorize]` 어트리뷰트를 추가**하면, 앞서 설정한 토큰 인증이 수행된다.

다음은 컨트롤러 액션에 `[Authorize]` 어트리뷰트를 적용한 예시이다.

![](images/Blazor_Auth/014.png)

<br>

---

# Client : 토큰 관리

## 세션 스토리지

세션 스토리지는 **브라우저의 탭 단위에서 데이터를 저장할 수 있는 공간**이다.<br>
브라우저를 닫거나 해당 탭을 닫으면 저장 데이터가 사라진다.

`JSRuntime` 서비스를 통해 세션 스토리지에 토큰을 저장, 수정, 삭제할 수 있다.<br>
세션 스토리지에는 key-value 형태로 값을 저장할 수 있으며, 사용 방식은 아래와 같다.

```csharp
await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "Key값", Value); // 데이터 저장
await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "Key값");    // 데이터 로드
```

<br>

---

## 헤더에 토큰 추가

Blazor 클라이언트에서 서버로 요청을 보낼 때, Access Token과 Refresh Token<br>
두 가지 모두를 헤더에 추가해야 한다.

**이때, 인증 토큰은 `Authorization` 헤더에 추가해야 한다.**<br>

헤더에 토큰을 저장하는 방법은 아래와 같다.<br>

```csharp
var requestMessage = new HttpRequestMessage(HttpMethod.Post, ApiPath);  // Request 메시지

requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);  // 인증 토큰은 Authorization 헤더에 추가

requestMessage.Headers.Add("헤더명", "Value");                          // Header에 데이터 추가
requestMessage.Headers.Remove("헤더명");                                // Header에서 데이터 삭제
```

<br>

---

## 헤더에서 재발급 토큰 로드

Access Token이 만료되었지만 Refresh Token이 유효하여 Access Token을 재발급받은 경우,<br>
본 프로젝트의 서버는 `X-NEW-ACCESS-TOKEN` 헤더에 재발급한 토큰을 추가해 전송한다.

헤더에서 값을 가져오는 방법은 아래와 같다.

```csharp
res.Headers.TryGetValues("X-NEW-ACCESS-TOKEN", out var newAccessToken); // out 파라미터 newAccessToken으로 값을 로드
```

실제 값은 out 파라미터로 로드한 변수의 Value 필드에 저장되어있다.<br>
따라서 다음과 같이 사용 가능하다.

```csharp
Console.WriteLine(newAccessToken.Value);
```

<br>

---

# 예시 프로젝트 상세설명

## 페이지 공통

API 통신이 필요할 때 해당하는 `Service`의 함수를 호출한다.<br>
각 서비스는 Blazor Server로 API를 호출하고, Blazor Server는 상황에 따라 **인증 처리를 한 후 응답**을 보낸다.

Blazor Server에서 제공하는 데이터는 실제 DB와 연동되지 않은 더미 데이터이다.

**API를 호출하거나, 페이지에 진입할 때마다 토큰이 유효한지 인증**한다.<br>
인증에 실패하면 세션 스토리지의 토큰이 무효화되고, 로그인 화면으로 강제 이동한다.

<br>

---

## 최상위 페이지 : AuthPage.razor

본 프로젝트는 로그인 이후의 모든 페이지에 진입할 때마다 토큰 인증을 진행하고 있다.<br>
따라서 모든 페이지가 상속하는 최상위 페이지에 인증 로직을 구현했다.

별도의 View(화면 구성 요소)는 존재하지 않고, 토큰 인증을 위한 `@code` 로직만 존재한다.

```csharp
@inject IConfirmService ConfirmService
@inject NavigationManager NavigationManager
@inject Blazored.SessionStorage.ISessionStorageService sessionStorage

@inject AuthService AuthService
@inject IJSRuntime jsRuntime


@code {
    // 페이지가 렌더링되기 전에 수행
    protected override async Task OnInitializedAsync()
    {
        var (verified, resetToken) = await CheckSession();

        if (verified == false)
        {
            await MoveToLogin(resetToken);
        }
    }

    // 세션 스토리지의 토큰을 무효화하고 로그인 페이지로 강제 이동
    protected async Task MoveToLogin(bool resetToken = false)
    {
        if (resetToken)
        {
            await sessionStorage.SetItemAsync<string>("accesstoken", "");
            await sessionStorage.SetItemAsync<string>("refreshtoken", "");
        }
        NavigationManager.NavigateTo("/", true);
    }

    // 세션 토큰이 유효한지 확인
    async Task<(bool, bool)> CheckSession()
    {
        var accessToken = await sessionStorage.GetItemAsync<string>("accesstoken");
        if (accessToken == null || accessToken.Equals(""))
        {
            return (false, false);
        }

        var verified = await AuthService.CheckToken();
        return (verified == ErrorCode.None, true);
    }
}
```

인증이 필요한 모든 페이지(로그인 화면을 제외한 모든 페이지)는 AuthPage를 상속받아 다음과 같이 사용한다.

```csharp
@inherits AuthPage

// ...

@code {
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        // 이후 동작 정의
    }
}
```

상속받은 페이지에서 `OnInitializedAsync` 단계에 수행해야할 작업이 있다면<br>
`base`의 `OnInitializedAsync`를 먼저 호출한 후 수행해야 한다.

<br>

---

## 로그인 페이지 : Login.razor

![](images/Blazor_Auth/001.png)

### 사용된 주요 컴포넌트

- **GridRow** (Ant Design)
  ```html
  <GridRow Justify="center">
    <GridCol Span="5">
      <!-- ...그리드 안에 들어갈 View... -->
    </GridCol>
  </GridRow>
  ```

`AntDesign` 라이브러리의 Grid 시스템을 사용해 화면을 구성했다.<br>

`AntDesign`의 Grid는 24등분을 기반으로 한다.<br>
`Span`에 지정한 값이 24등분한 Grid 내에서의 크기가 된다.<br>

위 예시 코드에서 `GridCol` 요소는 24등분 그리드를 가로로 5칸 차지하는 크기를 갖게 된다.<br>
가로로 한 줄을 전부 차지하게 하려면 Span값을 24로 지정하면 된다.

`Justify`는 해당 구성 요소의 정렬을 지정한다. `center`로 하면 상위 요소의 가운데 위치에 정렬된다.

- **Card** (Ant Design)

  ```csharp
  <Card Title="Login" Style="width:100;">
    <Body>
      <!-- ...카드 바디 안에 들어갈 View... -->
    </Body>
  </Card>
  ```

`AntDesign` 라이브러리의 Card 컴포넌트를 사용해 로그인 창을 구성했다.<br>
Card는 `Title`과 `<Body>`로 이루어진 직사각형 구성 요소이다.

Title에 문자열을 지정해 상단의 제목을 설정할 수 있다.

- **Input** (Ant Design)

  ```html
  <AntDesign.Input Placeholder="Email" @bind-Value="@email">
    <Prefix>
      <Icon Type="user" />
    </Prefix>
  </AntDesign.Input>

  @code { public string email { get; set; } = string.Empty; }
  ```

`AntDesign` 라이브러리의 Input 컴포넌트를 사용해 이메일, 비밀번호 입력 칸을 구성했다.<br>
Input 태그 내부에 `<Prefix>`와 `<Icon>`을 사용해 칸 왼쪽에 아이콘을 설정할 수 있다.<br>

설정할 수 있는 아이콘은 라이브러리 문서에서 확인 가능하다.

[참고 문서 : AntDesign Icon](https://ant.design/components/icon)

`Placeholder`에 지정한 문구는 Input이 비어있을 때에 나타난다.<br>
Input과 바인딩될 변수는 `@bind-Value`에 지정할 수 있다.

- **Button** (Ant Design)

  ```csharp
  <Button Type="@AntDesign.ButtonType.Primary" @onclick="OnClickLogin" Loading="loading">
  				Login
  </Button>

  @code {
    public bool loading { get; set; } = false;

    async Task OnClickLogin()
    {
        // 빈 칸이 있을 때
        if (email.Equals("") || password.Equals(""))
        {
            await ConfirmService.Show("이메일과 비밀번호를 입력해주세요.", "Error", ConfirmButtons.OK);
            return;
        }

        // Auth Service에 로그인 요청
        loading = true;
        var loginResult = await AuthService.Login(email, password);
        loading = false;

        // 로그인 실패
        if (loginResult == null || loginResult.Result != ErrorCode.None)
        {
            await ConfirmService.Show("로그인에 실패했습니다. 이메일과 비밀번호를 다시 확인해주세요.", "Error", ConfirmButtons.OK);
            return;
        }

        // 로그인 성공
        await sessionStorage.SetItemAsStringAsync("accesstoken", loginResult.accessToken);
        await sessionStorage.SetItemAsStringAsync("refreshtoken", loginResult.refreshToken);

        NavigationManager.NavigateTo("/Lookup_Specific_User");
    }
  }
  ```

`AntDesign` 라이브러리의 Button 컴포넌트를 사용해 로그인 요청 버튼을 구성했다.<br>

**버튼 색상**은 `Type`으로 지정 가능하다.<br>
버튼에 **로딩 효과**를 주고싶다면 `Loading`에 bool타입 변수를 지정해 구현할 수 있다.

버튼 클릭 시에 바인드할 함수는 `@onclick`에 지정 가능하다.<br>

- **ConfirmService** (Ant Design)

  ```csharp
  @inject IConfirmService ConfirmService

  @code {
    await ConfirmService.Show("로그인에 실패했습니다. 이메일과 비밀번호를 다시 확인해주세요.", "Error", ConfirmButtons.OK);
  }
  ```

`AntDesign` 라이브러리의 `ConfirmService`를 주입받아 모달 창을 띄울 수 있다.<br>

<br>

---

## 로그인 페이지 레이아웃 : MainLayout.razor

![](images/Blazor_Auth/004.png)

```csharp
@inherits LayoutComponentBase

<div style="background-color:#001529; height:100vh;">
    <main>
        <article class="content px-4">
            @Body
        </article>
    </main>
</div>

<RadzenDialog />
<RadzenNotification/>
<RadzenContextMenu/>
<RadzenTooltip/>
```

배경색을 남색으로 지정하고, div의 높이 `height`를 `100vh`로 지정했다.<br>
화면 전체가 남색으로 가득차게 하기 위함이다.

---

## 로그인 이후 페이지 레이아웃 : AfterLoginLayout.razor

![](images/Blazor_Auth/002.png)

```csharp
@inherits LayoutComponentBase

<div>
    <NavMenu />

    <main>
        <article class="content px-4">
            @Body
        </article>
    </main>
</div>

<RadzenDialog />
<RadzenNotification />
<RadzenContextMenu />
<RadzenTooltip />
```

`NavMenu` 컴포넌트를 상단에 가지고 있고, 하단에는 각 View가 배치되는 구성이다.

---

## 네비게이션 바 레이아웃 : NavMenu.razor

```csharp
@inject NavigationManager NavigationManager
@inject Blazored.SessionStorage.ISessionStorageService sessionStorage

<Header Class="header" Style="width:100%">
    <div style="display:inline-block; margin-right:1.5vw;">
        <h4 style="color:white"><Icon Type="setting" Theme="outline" /> ManagingTool</h4>
    </div>
    <Menu Theme="MenuTheme.Dark" Mode="MenuMode.Horizontal" Style="display:inline-block">
        <MenuItem Key="1" RouterLink="/Lookup_Specific_User">
            <Icon Type="user" Theme="outline" />
            Lookup Specific User
        </MenuItem>
        <MenuItem Key="2" RouterLink="/Lookup_Multiple_Users">
            <Icon Type="user" Theme="outline" />
            Lookup Multiple Users
        </MenuItem>
        <MenuItem Key="3" @onclick="Logout">
            <Icon Type="logout" Theme="outline" />
            Logout
        </MenuItem>
    </Menu>
</Header>

@code {

    async Task Logout()
    {
        await sessionStorage.RemoveItemAsync("accesstoken");
        await sessionStorage.RemoveItemAsync("refreshtoken");
        NavigationManager.NavigateTo("/");
    }
}
```

상단에 수평 네비게이션 바 형태로 배치시키기 위해 `AntDesign`에서 제공하는 템플릿을 사용했다.<br>

### 사용한 주요 컴포넌트

- **Menu** (Ant Design)

```csharp
<Menu Theme="MenuTheme.Dark" Mode="MenuMode.Horizontal" Style="display:inline-block">
    <MenuItem>
        <!-- ...MenuItem 내부 구성 요소... -->
    </MenuItem>
</Menu>
```

`Theme`에 테마 타입을 지정할 수 있다.<br>
`Mode`에 Horizontal, Vertical 등 메뉴 배치 모드를 지정할 수 있다.<br>

내부에 `MenuItem` 컴포넌트를 복수개 가질 수 있는 구조이다.

- **MenuItem** (Ant Design)

```csharp
<MenuItem Key="1" RouterLink="/Lookup_Specific_User">
    <Icon Type="user" Theme="outline" />
    Lookup Specific User
</MenuItem>
```

`RouterLink`에 해당 메뉴를 누르면 어느 페이지로 이동시킬지 경로를 지정한다.<br>
`Key`값으로 메뉴마다 식별 가능한 값을 지정할 수 있다.<br>

`MenuItem` 내부에 아이콘과 메뉴 이름을 설정할 수 있다.

```csharp
<MenuItem Key="3" @onclick="Logout">
    <Icon Type="logout" Theme="outline" />
    Logout
</MenuItem>

@code {
    async Task Logout()
    {
        await sessionStorage.RemoveItemAsync("accesstoken");
        await sessionStorage.RemoveItemAsync("refreshtoken");
        NavigationManager.NavigateTo("/");
    }
}
```

`RouterLink`를 지정하지 않거나, `@onclick`으로 클릭 시의 함수를 바인드할 수 있다.<br>
본 프로젝트에서는 Logout 메뉴에 대해 별도의 경로를 지정하지 않고, 함수 수행 후 로그인 화면으로 돌아가도록 처리했다.

<br>

---

## 서비스 공통 : BaseService.cs

클라이언트에서 서버로 요청을 보낼 때, 세션 스토리지에 있는 두 가지 토큰을 모두 헤더에 추가해서 전송해야 한다.

이에 대한 공통 함수들을 BaseService에 구현했으며, 모든 서비스는 BaseService를 상속받아<br>
하단의 기능을 공유한다.

- `CreateReqMsg`

  ```csharp
  protected async Task<HttpRequestMessage> CreateReqMsg(HttpMethod method, string path, object? body, bool addHeader = true)
  {
      var requestMessage = new HttpRequestMessage(method, path);

      // Body 직렬화
      if (body != null)
      {
          SerializeReqBody(ref requestMessage, body);
      }

      // 헤더에 토큰 추가
      if (addHeader)
      {
          var (accessToken, refreshToken) = await _tokenManager.GetTokensFromSessionStorage();
          AttachTokensToRequestHeader(ref requestMessage, accessToken, refreshToken);
      }

      return requestMessage;
  }
  ```

  Http 메소드 타입, API 경로, 직렬화할 JSON Body, 헤더 추가 여부를 매개변수로 받아 RequestMessage를 생성한다.

- `SerializeReqBody`

  ```csharp
  // Request Body를 JSON 직렬화하여 Body에 저장
  void SerializeReqBody(ref HttpRequestMessage reqMsg, object reqBody)
  {
      string requestBody = JsonSerializer.Serialize(reqBody);
      reqMsg.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
  }
  ```

  Object를 Json 직렬화한 다음 요청 바디에 저장한다.

- `AttachTokensToRequestHeader`
  ```csharp
  // AccessToken과 RefreshToken을 RequestMessage 헤더에 추가
  protected void AttachTokensToRequestHeader(ref HttpRequestMessage req, string accessToken, string refreshToken)
  {
      req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
      req.Headers.Remove("refresh_token");
      req.Headers.Add("refresh_token", refreshToken);
  }
  ```
  요청 헤더에 토큰을 추가한다.

<br>

---

## AuthService.cs

ManagingTool 로그인과 토큰 유효성 검사 요청을 담당한다.

- `CheckToken`

  헤더에 토큰을 추가해 GET 메소드로 검사 요청을 보낸다. 실패 시 `ErrorCode.Unauthorized`를 반환한다.

- `Login`

  이메일과 패스워드를 전송받아 POST 메소드로 로그인을 시도한다. 헤더에 토큰을 붙이지 않아도 된다.

---

## UserService.cs

로그인 이후 운영 API를 호출할 때 사용되는 서비스이다.<br>
유저 데이터에 대한 API 호출을 담당한다. 제공되는 데이터는 모두 더미 데이터이다.

모든 요청의 헤더에 토큰 인증이 필요하다.

- `GetUserBasicInfo`

  UserID를 받아, 해당하는 유저의 기본 정보를 불러온다.

- `GetMultipleUserBasicInfo`

  Category와 Max, Min Value를 받아 범위에 해당하는 유저들의 정보를 불러온다.

- `GetUserItemList`

  UserID를 받아 해당하는 유저의 아이템 목록을 가져온다.

- `GetUserMailList`

  UserID를 받아 해당하는 유저의 메일 목록을 가져온다.

---

## 서버 인증 옵션 및 핸들러 정의 : JwtBearerConfig.cs

ManagingTool.Server에서 인증에 사용되는 `TokenValidationParameters` 및<br>
인증 이벤트 핸들러를 정의한 파일이다.

- 인증 옵션

  ```csharp
  public TokenValidationParameters tokenValidatedParameters { get; }

  public JwtBearerConfig()
  {
      // 인증 옵션 파라미터 정의
      tokenValidatedParameters = new TokenValidationParameters
      {
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateIssuerSigningKey = true,
          ValidateLifetime = true,    // 토큰 유효성 검증 여부
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SigningKey_Com2us")) // 비밀 서명 키
      };
  }
  ```

- 인증 실패 시 핸들러

  ```csharp
  public void OnAuthenticationFailedHandler(AuthenticationFailedContext context, JwtBearerOptions options)
  {
      if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
      {
          // 리프레시 토큰 가져오기
          if (GetRefreshToken(context, out var refreshToken) == false)
          {
              context.Response.StatusCode = 401;
              return;
          }

          // 리프레시 토큰이 만료되었는지 확인
          if (IsExpiredToken(context, refreshToken, options))
          {
              context.Response.StatusCode = 401;
              return;
          }

          // 리프레시 토큰에서 AccountId 가져오기
          var accountId = TokenManager.GetClaim(refreshToken);
          if (accountId == 0)
          {
              context.Response.StatusCode = 401;
              return;
          }

          // DB의 RefreshToken과 비교
          if (AreEqualWithDBRefreshToken(accountId, refreshToken) == false)
          {
              context.Response.StatusCode = 401;
              return;
          }

          // 새 액세스 토큰 발급
          string newAccessToken = TokenManager.CreateToken(true, accountId);
          context.Response.Headers.Add("X-NEW-ACCESS-TOKEN", newAccessToken);

          // 요청 정상 수행
          ClaimsIdentity claims = new ClaimsIdentity(new[]
          {
              new Claim("AccountId", accountId.ToString()),
          }, JwtBearerDefaults.AuthenticationScheme);

          context.Principal = new ClaimsPrincipal(new ClaimsIdentity[] { claims });

          context.Success();
      }
  }
  ```

  `Authorization` 헤더에 담긴 Access Token 인증이 실패했을 경우에 수행되는 핸들러이다.<br>
  Refresh Token이 유효하다면 새 Access Token을 재발급하고, 요청을 정상 처리한다.

<br>

---

## 서버 토큰 관리 클래스 : TokenManager.cs

ManagingTool.Server의 `TokenManager`는 토큰을 생성하고, 토큰에 담긴 Claim을 추출할 수 있다.

- `CreateTokens`

  ```csharp
  // AccessToken과 RefreshToken 생성
  public static Tuple<string, string> CreateTokens(Int64 accountId)
  {
      var accessToken = CreateToken(true, accountId);
      var refreshToken = CreateToken(false, accountId);

      return new Tuple<string, string>(accessToken, refreshToken);
  }
  ```

  Access Token과 Refresh Token을 생성해 리턴한다.

- `CreateToken`

  ```csharp
  // 토큰 종류에 따라 유효시간을 정하여 생성
  public static string CreateToken(bool isAccessToken, Int64 accountId)
  {
      var tokenHandler = new JwtSecurityTokenHandler();

      // Signing Key와 만료기간 설정
      var key = Encoding.ASCII.GetBytes(_signingKey);
      var expires = isAccessToken ? DateTime.UtcNow.AddHours(1) : DateTime.UtcNow.AddHours(6);

      // 토큰 구조체 정의
      var tokenDescriptor = new SecurityTokenDescriptor
      {
          Subject = new ClaimsIdentity(new Claim[]
          {
          new Claim("AccountId", accountId.ToString()),
          }),
          Expires = expires,
          SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      // 토큰 생성
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
  }
  ```

  토큰 종류에 따라 만료 기간을 다르게 하여 토큰을 생성한다.

- `GetClaim`

  ```csharp
  // 토큰에 담긴 정보(Claim) 추출
  public static Int64 GetClaim(string token)
  {
      var handler = new JwtSecurityTokenHandler();
      var refreshToken = handler.ReadJwtToken(token);

      // AccountId Claim 추출
      var accountIdClaim = refreshToken.Claims.FirstOrDefault(claim => claim.Type.Equals("AccountId"));

      if (accountIdClaim != null)
      {
          return Int64.Parse(accountIdClaim.Value);
      }

      return 0;
  }
  ```

  토큰의 페이로드에 담긴 Claim을 추출해낸다.

<br>

---
