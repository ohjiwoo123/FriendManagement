# FriendManagement
## C, C++에 이어서 이번엔 C#으로 주소록 프로그램 만들기
## 어려웠던 점
C와 C++과 구조가 달라서 어느 부분에 class를 넣어야 하고 어디에 new로 할당하고 그런 부분이 헷갈렸다.  
C#은 전역이라는 개념이 없어서 static과 멤버(class)를 잘 활용하여야 했다.  
class로 많은 것을 처리하는 것을 보니 Java 언어와 비슷한 점이 많은 것 같다.  
아직 기본적인 private public protected 등의 개념이 부족한 것 같다.  

```
namespace
{
  internal class
  {
    static void Main(string[] args)
    {
      // 메인함수 내용
    }
  }
}
```

여기에서 추가로 internal class 와 동급의 위치에서 추가적인 class를 만든다. ex) class Friend  
interclass 안에서 다시 class를 만들면..? 
